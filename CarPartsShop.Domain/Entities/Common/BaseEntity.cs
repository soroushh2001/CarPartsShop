using System.ComponentModel.DataAnnotations;

namespace CarPartsShop.Domain.Entities.Common
{
    public class BaseEntity 
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }
    }
}
