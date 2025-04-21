using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PhalconSoft.Models;

public partial class BlogContext : DbContext
{

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }

    public virtual DbSet<Portfolio> Portfolios { get; set; }

    public virtual DbSet<PortfolioPhoto> PortfolioPhotos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<string> SendEmails { get; set; }
    
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PhalconSoftDb;User Id=sa;Password=<YourPassword>;TrustServerCertificate=True;");

  
}
