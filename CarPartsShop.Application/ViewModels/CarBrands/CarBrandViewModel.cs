using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.CarBrands
{
    public class CarBrandViewModel
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public int? ParentId { get; set; }
    }
}
