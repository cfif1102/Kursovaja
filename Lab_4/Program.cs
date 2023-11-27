using Lab_4.Data;
using Lab_4.Middleware;
using Lab_4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var Configuration = builder.Configuration;
var services = builder.Services;

string connection = Configuration.GetConnectionString("DefaultConnection");

services.AddDbContext<StudentsContext>(options => options.UseSqlServer(connection));

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Projects.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
    options.Cookie.IsEssential = true;
});

builder.Services.AddResponseCaching();

builder.Services.AddControllersWithViews(options =>
{
    options.CacheProfiles.Add("CachedDefault",
        new CacheProfile()
        {
            Duration = 242,
            Location = ResponseCacheLocation.Any,
        });
});

builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 5;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddUserManager<UserManager<User>>()
    .AddEntityFrameworkStores<StudentsContext>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[]
{
    new CultureInfo("en-US"),
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();



app.UseDbInitializer();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
