using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Payment
{
    public class VerifyParameters
    {
        public string amount {   set; get; }
        public string merchant_id {   set; get; }
        public string authority {   set; get; }
    }
}
