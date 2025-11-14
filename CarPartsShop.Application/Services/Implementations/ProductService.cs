using CarPartsShop.Application.Extensions;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Application.Statics;
using CarPartsShop.Application.ViewModels.Common;
using CarPartsShop.Application.ViewModels.Products;
using CarPartsShop.Domain.Entities.Order;
using CarPartsShop.Domain.Entities.Shop;
using CarPartsShop.Domain.Interfaces;

namespace CarPartsShop.Application.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<FilterProductViewModel> FilterProductAsync(FilterProductViewModel filter)
        {
            var query = _productRepository.GetAllProducts();

            if (filter.CategoryUrls != null)
            {
                query = query.Where(p =>
                    p.ProductCategories != null &&
                    p.ProductCategories.Any(pc =>
                        pc.Category != null && filter.CategoryUrls.Contains(pc.Category.Slug)));
            }

            if (filter.CategoryTitle != null)
            {
                query = query.Where(x => x.ProductCategories.Any(x => x.Category.Title == filter.CategoryTitle));
            }

            if (filter.BrandTitle != null)
            {
                query = query.Where(x => x.ProductCarBrands.Any(x => x.CarBrand.Slug == filter.BrandTitle));
            }

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            switch (filter.ProductQualityForFilter)
            {
                case ProductQualityForFilter.All:
                    break;
                case ProductQualityForFilter.Original:
                    query = query.Where(x => x.Quality == ProductQuality.Original);
                    break;
                case ProductQualityForFilter.Corporate:
                    query = query.Where(x => x.Quality == ProductQuality.Corporate);
                    break;
                case ProductQualityForFilter.Others:
                    query = query.Where(x => x.Quality == ProductQuality.Others);
                    break;
            }

            if (filter.IsExisted != null)
            {
                query = query.Where(x => x.IsExisted == filter.IsExisted);
            }

            if (filter.Search != null)
            {
                filter.SortBy = ProductSortBy.Title;
                query = query.Where(p => p.Title.Contains(filter.Search))
                    .OrderByDescending(p => p.Title.StartsWith(filter.Search) ? 1 : 0);
            }
            else
            {
                switch (filter.SortBy)
                {
                    case ProductSortBy.Date:
                        query = filter.OrderBy == ProductOrderBy.Desc
                            ? query.OrderByDescending(x => x.LastModifiedDate)
                            : query.OrderBy(x => x.LastModifiedDate);
                        break;
                    case ProductSortBy.Price:
                        query = filter.OrderBy == ProductOrderBy.Desc
                            ? query.OrderByDescending(x => x.Price)
                            : query.OrderBy(x => x.Price);
                        break;
                    case ProductSortBy.Title:
                        query = filter.OrderBy == ProductOrderBy.Desc
                            ? query.OrderByDescending(x => x.Title)
                            : query.OrderBy(x => x.Title);
                        break;
                    case ProductSortBy.BestSeller:
                        query = filter.OrderBy == ProductOrderBy.Desc
                            ? query.OrderByDescending(x => x.OrderDetails
                                .Where(od => od.Order.Status != OrderStatus.WaitToPayment)
                                .Sum(od => od.Count))
                            : query.OrderBy(x => x.OrderDetails
                                .Where(od => od.Order.Status != OrderStatus.WaitToPayment)
                                .Sum(od => od.Count));
                        break;
                }

            }
            var items = query.Select(x => new ProductsListViewModel
            {
                Id = x.Id,
                Price = x.Price,
                LastModifiedDate = x.LastModifiedDate,
                CreatedDate = x.CreatedDate,
                Quality = x.Quality,
                Title = x.Title,
                IsExisted = x.IsExisted,
                Categories = x.ProductCategories.Where(x => x.ProductId == x.Id).Select(x => x.Category).ToList(),
                MainImage = x.MainImage,
                Slug = x.Slug
            }).AsQueryable();

            await filter.SetPaging(items);
            return filter;
        }

        public async Task<CreateProductResult> CreateProductAsync(CreateProductViewModel createProductViewModel)
        {
            var checkTitle = await _productRepository.IsProductTitleExistedAsync(createProductViewModel.Title);
            if (checkTitle) return CreateProductResult.InvalidTitle;

            var newProduct = new Product
            {
                Title = createProductViewModel.Title,
                CreatedDate = DateTime.Now,
                Description = createProductViewModel.Description,
                LastModifiedDate = DateTime.Now,
                Price = createProductViewModel.Price,
                IsExisted = createProductViewModel.IsExisted,
                Quality = createProductViewModel.ProductQuality,
                ShortDescription = createProductViewModel.ShortDescription,
                Slug = NameGenerator.GenerateSlug(createProductViewModel.Title),
            };

            var imageName = createProductViewModel.MainImage.FileNameGenerator();

            var upResult = await createProductViewModel.MainImage.UploadImage(imageName, StaticDetails.ProductOrgServerPath,
                StaticDetails.ProductThumbServerPath);

            if (!upResult)
                return CreateProductResult.InvalidImage;

            newProduct.MainImage = imageName;

            await _productRepository.AddProductAsync(newProduct);
            await _productRepository.SaveChangesAsync();
            await AddCategoriesToProductAsync(createProductViewModel.SelecetCategoryIds, newProduct.Id);
            await AddBrandToProductAsync(createProductViewModel.SelecetedBrandsIds, newProduct.Id);
            return CreateProductResult.Success;
        }

        public async Task AddCategoriesToProductAsync(List<int> categoriesIds, int productId)
        {
            var categories = categoriesIds.Select(x => new ProductCategory()
            {
                ProductId = productId,
                CategoryId = x
            }).ToList();

            await _productRepository.AddCategoriesToProductAsync(categories);
            await _productRepository.SaveChangesAsync();
        }

        public async Task AddBrandToProductAsync(List<int> brandsIds, int productId)
        {
            var brands = brandsIds.Select(x => new ProductCarBrand()
            {
                ProductId = productId,
                CarBrandId = x
            }).ToList();

            await _productRepository.AddCarBrandsToProductAsync(brands);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<EditProductViewModel?> GetProductToEditAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
                return null;
            return new EditProductViewModel
            {
                Price = product.Price,
                CurrentMainImage = product.MainImage,
                Description = product.Description,
                ProductQuality = product.Quality,
                ShortDescription = product.ShortDescription,
                Title = product.Title,
                IsExisted = product.IsExisted,
                SelecetCategoryIds = await _productRepository.GetSelectedCategoriesIdsAsync(productId),
                SelecetedBrandsIds = await _productRepository.GetSelectedBrandsIdsAsync(productId)
            };
        }

        public async Task<EditProductResult> EditProductAsync(EditProductViewModel edit)
        {
            var productToEdit = await _productRepository.GetProductByIdAsync(edit.Id);
            if (productToEdit == null) return EditProductResult.NotFound;
            productToEdit.ShortDescription = edit.ShortDescription;
            productToEdit.Title = edit.Title;
            productToEdit.Description = edit.Description;
            productToEdit.Price = edit.Price;
            productToEdit.LastModifiedDate = DateTime.Now;
            productToEdit.IsExisted = edit.IsExisted;
            productToEdit.Quality = edit.ProductQuality;
            productToEdit.Slug = edit.Title.GenerateSlug();

            if (edit.MainImage != null)
            {
                productToEdit.MainImage.DeleteImage(StaticDetails.ProductOrgServerPath, StaticDetails.ProductThumbServerPath);
                var newImageName = edit.MainImage.FileNameGenerator();
                var upResult = await edit.MainImage.UploadImage(newImageName, StaticDetails.ProductOrgServerPath, StaticDetails.ProductThumbServerPath);
                if (!upResult)
                    return EditProductResult.InvalidImage;
                productToEdit.MainImage = newImageName;

            }

            _productRepository.UpdateProduct(productToEdit);
            await _productRepository.SaveChangesAsync();
            await _productRepository.RemoveCategoriesFromProductAsync(productToEdit.Id);
            await _productRepository.SaveChangesAsync();
            await AddCategoriesToProductAsync(edit.SelecetCategoryIds, productToEdit.Id);
            await _productRepository.RemoveCarBrandsFromProductAsync(productToEdit.Id);
            await _productRepository.SaveChangesAsync();
            await AddBrandToProductAsync(edit.SelecetedBrandsIds, productToEdit.Id);
            return EditProductResult.Success;
        }

        public async Task<bool> ToggleProductStatusAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null) return false;
            product.IsExisted = !product.IsExisted;
            _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductViewModel>> GetLatestProductAsync(int size = 5)
        {
            var latestProducts = await _productRepository.GetLatestProductAsync(size);
            return latestProducts.Select(x => new ProductViewModel
            {
                Description = x.Description,
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
                Title = x.Title,
                MainImage = x.MainImage,
                Price = x.Price,
                IsExisted = x.IsExisted,
                Id = x.Id,
            }).ToList();
        }

        public async Task<ProductViewModel?> GetProductBySlugAsync(string slug)
        {
            var product = await _productRepository.GetProductBySlug(slug);
            if (product == null)
            {
                return null;
            }
            return new ProductViewModel
            {
                Categories = await _productRepository.GetSelectedCategoriesAsync(product.Id),
                CarBrands = await _productRepository.GetSelectedBrandsAsync(product.Id),
                Description = product.Description,
                IsExisted = product.IsExisted,
                MainImage = product.MainImage,
                Price = product.Price,
                ShortDescription = product.ShortDescription,
                Title = product.Title,
                ProductQuality = product.Quality,
                Id = product.Id,
                Slug = product.Slug
            };
        }

        public async Task<List<ProductViewModel>> GetRelatedProductsAsync(int productId, int size = 5)
        {
            var brandIds = await _productRepository.GetBrandIdsByProductIdAsync(productId);
            var products = await _productRepository.GetProductsByBrandIdsAsync(brandIds, productId, size);
            return products.Select(x => new ProductViewModel
            {
                Title = x.Title,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Id = x.Id,
                MainImage = x.MainImage,
                IsExisted = x.IsExisted,
                Price = x.Price,
            }).ToList();
        }

        public async Task<List<ProductViewModel>> GetBestSellerProductsAsync(int size = 5)
        {
            var products = await _productRepository.GetBestSellerProductsAsync(size);
            return products.Select(x => new ProductViewModel
            {
                Title = x.Title,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Id = x.Id,
                MainImage = x.MainImage,
                IsExisted = x.IsExisted,
                Price = x.Price,
            }).ToList();
        }
    }
}
