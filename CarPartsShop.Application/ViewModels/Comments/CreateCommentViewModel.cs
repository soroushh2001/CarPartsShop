using CarPartsShop.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Comments
{
    public class CreateCommentViewModel
    {
        public int ProductId { get; set; }
        public string Message { get; set; }

        public string? ProductSlug { get; set; } 
    }
}
