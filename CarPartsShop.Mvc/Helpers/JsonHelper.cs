using Microsoft.AspNetCore.Mvc;

namespace CarPartsShop.Mvc.Helpers
{
    public static class JsonHelper
    {
        public static JsonResult JsonResponse(int code, string message)
        {
            return new JsonResult(new {status= code,message = message});
        }

    }
}
