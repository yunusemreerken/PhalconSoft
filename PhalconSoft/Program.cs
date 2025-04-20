using Microsoft.EntityFrameworkCore;
using PhalconSoft.Helpers;
using PhalconSoft.Models;

var builder = WebApplication.CreateBuilder(args);

// Bağlantı cümlesini al
var connectionString = builder.Configuration.GetConnectionString("Default");

// DbContext yapılandırması
builder.Services.AddDbContext<PhalconsoftContext>(options =>
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
builder.Services.AddControllersWithViews();

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

app.UseRouting();

// Global hata yakalama middleware’i
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthorization();

// Area route tanımı (Admin alanı için)
app.MapAreaControllerRoute(
    name: "adminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);

// Varsayılan route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Uygulamayı çalıştır
app.Run();