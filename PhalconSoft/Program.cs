using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.EntityFrameworkCore;
using PhalconSoft.Helpers;
using PhalconSoft.Models;

var builder = WebApplication.CreateBuilder(args);

// Bağlantı cümlesini al
var connectionString = builder.Configuration.GetConnectionString("Default");

// DbContext yapılandırması
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });

    // Sadece geliştirme ortamında loglama ve detaylı hata
    if (builder.Environment.IsDevelopment())
    {
        options
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }
});

// MVC Controller ekleniyor
builder.Services.AddControllersWithViews().
    AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
builder.Services.AddNotyf(config=> { config.DurationInSeconds = 10;config.IsDismissable = true;config.Position = NotyfPosition.BottomRight; });
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.Secure = CookieSecurePolicy.Always;
});

// Uygulama oluşturuluyor
var app = builder.Build();

// Hata yönetimi ve güvenlik ayarları
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy(); // cookie ayarlarını uyguluyor

app.UseRouting();

// Global hata yakalama middleware’i
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
    // Area route tanımı (Admin alanı için)
    endpoints.MapAreaControllerRoute(
        name: "adminArea",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );
});



app.UseNotyf();



// Uygulamayı çalıştır
app.Run();