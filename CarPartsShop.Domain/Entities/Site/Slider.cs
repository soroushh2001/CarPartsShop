using CarPartsShop.Domain.Entities.Common;

namespace CarPartsShop.Domain.Entities.Site
{
    public class Slider : BaseEntity
    {
        public string ImageName { get; set; }
        public int Priority { get; set; }
    }
}
