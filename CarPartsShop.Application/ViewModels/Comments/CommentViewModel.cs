using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Comments
{
    public class CommentViewModel
    {
        public string UserEmail { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductName { get; set; }
    }
}
