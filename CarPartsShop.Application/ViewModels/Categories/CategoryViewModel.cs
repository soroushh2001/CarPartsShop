using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Application.ViewModels.Categories
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Title { get; set; } = null!;

        [MaxLength(250)]
        public string Slug { get; set; } = null!;

        public int? ParentId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
