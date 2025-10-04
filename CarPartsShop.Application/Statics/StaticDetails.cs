using Microsoft.Extensions.Configuration;

namespace CarPartsShop.Application.Statics;

public static class StaticDetails
{
    #region ProductImagePath

    public const string ProductOrgImagePath = "/contents/products/org/";

    public static readonly string ProductOrgServerPath =
        Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{ProductOrgImagePath}");

    public const string ProductThumbPath = "/contents/products/thumb/";

    public static readonly string ProductThumbServerPath =
        Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{ProductThumbPath}");


    #endregion

    #region SliderPath

     public const string SliderOrgImagePath = "/contents/slider/org/";

    public static readonly string SliderOrgServerPath =
        Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{SliderOrgImagePath}");

    public const string SliderThumbPath = "/contents/slider/thumb/";

    public static readonly string SliderThumbServerPath =
        Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{SliderThumbPath}");

    #endregion

    

}