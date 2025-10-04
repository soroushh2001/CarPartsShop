using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPartsShop.Application.ViewModels.Payment
{
    public class URLs
    {
        public const string gateWayUrl = "https://sandbox.zarinpal.com/pg/StartPay/";
        public const string requestUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
        public const string verifyUrl = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";


    }
}
