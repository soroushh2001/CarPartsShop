using CarPartsShop.Application.Extensions;
using CarPartsShop.Domain.Entities.Account;
using CarPartsShop.Infra.Data.Context;
using CarPartsShop.Infra.Ioc;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using CarPartsShop.Mvc.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();

var connectionString = builder.Configuration.GetConnectionString("CarPartsShopConnection");
builder.Services.AddDbContext<CarPartsShopDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(opt=>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

})
    .AddEntityFrameworkStores<CarPartsShopDbContext>()
    .AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityErrorDescriber>(); ;

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/login";
    opt.LogoutPath = "/logout";
    opt.AccessDeniedPath = "/login";
    opt.ExpireTimeSpan = TimeSpan.FromDays(30);
});


builder.Services.RegisterServices();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
