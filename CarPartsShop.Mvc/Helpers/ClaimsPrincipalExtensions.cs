using System.Security.Claims;

namespace CarPartsShop.Mvc.Helpers;

public static class ClaimsPrincipalExtensions
{
    public static int GetCurrentUserId(this ClaimsPrincipal principal)
    {
        var userId = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return int.Parse(userId);
    }

    public static string GetCurrentUserEmail(this ClaimsPrincipal principal)
    {
        var userEmail =  principal.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        return userEmail;
    }

    /*public static string? GetCurrentUserName(this ApplicationUser user)
    {
        if(user.FirstName!= null && user.LastName != null)
            return user.FirstName + " " + user.LastName;
        return user.Email;
    }
    */
}