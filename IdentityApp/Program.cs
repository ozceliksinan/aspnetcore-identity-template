using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// IEmailSender cagrildigi zaman SmtpEmailSender cagrilacak
// SMTP sunucu ayarlarini verelim
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
    new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"],
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:Username"],
        builder.Configuration["EmailSender:Password"])
);

builder.Services.AddControllersWithViews();

// Veritabaný yolunun belirlenmesi
// Context servis olarak kaydedilir.
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Options
// IdentityUser temel sinifini kullanmiyoruz. AppUser, temel sinif olan IdentityUser sinifina ekstra parametreler ekler.
// IdentityRole temel sinifini kullanmiyoruz. AppRole, temel sinif olan IdentityRole sinifina ekstra parametreler ekler.
// builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

// User Register Options
builder.Services.Configure<IdentityOptions>(options =>
{
    // Parola ayarlari
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    // Bir email sadece bir kisiye ait olsun
    options.User.RequireUniqueEmail = true;
    // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";

    // Parola yanlis girme operasyonlari
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;

    // Dogrulanmis mail hesabi olanlar Login olabilsin
    options.SignIn.RequireConfirmedEmail = true;
});

// Login islemlerinin ozellestirilmesi
builder.Services.ConfigureApplicationCookie(options =>
{
    // Kimlik dogrulama isteyen sayfalarýn yonlendirilmesi
    options.LoginPath = "/Account/Login";

    // Yetkisiz kimlige sahip kisinin yetkisiz sayfaya erisme istegi sonucu yonlendirilmesi
    options.AccessDeniedPath = "/Account/AccessDenied";

    // Verilen cerez suresi sonunda login islemi iptal edilir. True olursa sureyi otomatik uzerine ekler
    options.SlidingExpiration = true;

    // Uygulamaya giris yaptiktan sonra login islemi kayýt edilir.
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kimlik dogrulamasi icin
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// SeedData verilerini ekleme
IdentitySeedData.IdentityTestUser(app);

app.Run();