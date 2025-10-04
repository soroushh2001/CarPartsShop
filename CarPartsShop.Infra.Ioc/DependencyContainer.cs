using CarPartsShop.Application.Services.Implementations;
using CarPartsShop.Application.Services.Interfaces;
using CarPartsShop.Domain.Interfaces;
using CarPartsShop.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarPartsShop.Infra.Ioc
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Services

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICarBrandService, CarBrandService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ICommentService, CommentService>();
            #endregion


            #region Repositories

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICarBrandRepository, CarBrandRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();    
            #endregion

          
            return services;
        } 
    }
}
