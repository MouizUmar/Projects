using Microsoft.EntityFrameworkCore;
using System;
//1. Add Identity Namespace
using Microsoft.AspNetCore.Identity;
using MVCcoreDemo.Models; // 👈 YE LINE ADD KAREIN

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(model => model.UseSqlServer(builder.Configuration.GetConnectionString("GymConnectionString")));


//2. Add Authentication & Identity Services (EXISTING SESSION CODE KE UPAR)
builder.Services.AddAuthentication(options => {
    options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
}).AddIdentityCookies(); // 👈 YE ADD KAREIN

//builder.Services.AddIdentity<IdentityUser, IdentityRole>() // 👈 YE ADD KAREIN
//    .AddEntityFrameworkStores<ApplicationDbContext>() // 👈 (Ensure ApplicationDbContext exists)
//    .AddDefaultTokenProviders();

// 👇 Add session services (EXISTING CODE)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

//3. Add Authentication Middleware (EXISTING SESSION CODE KE UPAR)
app.UseAuthentication(); // 👈 YE LINE ADD KAREIN

// 👇 Add session middleware (EXISTING CODE)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();