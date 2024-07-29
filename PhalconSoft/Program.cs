using Microsoft.EntityFrameworkCore;
using PhalconSoft.Helpers;
using PhalconSoft.Models;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("Default");

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<PhalconsoftContext>(
    dbContextOptions => dbContextOptions
        .UseSqlServer(connectionString, 
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5, // Maximum number of retry attempts
                maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
                errorNumbersToAdd: null // Additional error numbers to include for retry
            );
        })
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)

        .EnableDetailedErrors()
);
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");//google harita görüntülenmesi için 
    await next();
});

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

app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseAuthorization();

app.MapAreaControllerRoute(
      name: "adminArea",
      areaName: "Admin",
      pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
   name: "site-haritasi",
   pattern: "/sitemap.xml",
   defaults: new { controller = "SiteMap", action = "Index" }
);

app.Run();
