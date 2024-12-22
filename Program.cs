using QLDaoTao.Data;
using QLDaoTao.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security;
using QLDaoTao.Areas.Admin.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStudent, ItemStudentService>();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));
// Cấu hình đăng nhập cho User
builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;

    })
   .AddEntityFrameworkStores<AppDbContext>()
   .AddDefaultTokenProviders()
   .AddDefaultUI();

// Cấu hình đăng nhập cho Student
//builder.Services.AddIdentity<AppStudent, IdentityRole>(
//    options =>
//    {
//        options.Password.RequiredUniqueChars = 0;
//        options.Password.RequireUppercase = false;
//        options.Password.RequiredLength = 8;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireLowercase = false;
//    })
//   .AddEntityFrameworkStores<AppDbContext>()
//   .AddDefaultTokenProviders()
//   .AddDefaultUI();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Đường dẫn đăng nhập cho User
    options.LoginPath = "/login";
    // Đường dẫn đăng nhập cho Student
    options.Cookie.Name = "StudentScheme";
    options.LoginPath = "/student/login";
    // Đường dẫn đăng nhập cho Admin
    options.Cookie.Name = "AdminScheme";
    options.LoginPath = "/Account/AdminLogin";
    //options.Cookie.Name = ".AspNetCore.Identity.Application";
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    //options.SlidingExpiration = true;
    options.AccessDeniedPath = "/admin/denied";


});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "LoginRedirect",
    pattern: "/admin/login",
    defaults: new { controller = "Account", action = "AdminLogin" });

app.MapControllerRoute(
    name: "Login",
    pattern: "/login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "StudentLogin",
    pattern: "/student/login",
    defaults: new { controller = "Student", action = "Login" });

app.MapControllerRoute(
    name: "AdminLogin",
    pattern: "/admin/login",
    defaults: new { controller = "Account", action = "AdminLogin" });


app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
