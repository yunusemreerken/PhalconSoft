using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PhalconSoft.Models;

public partial class PhalconsoftContext : DbContext
{
    public PhalconsoftContext()
    {
    }

    public PhalconsoftContext(DbContextOptions<PhalconsoftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }

    public virtual DbSet<Portfolio> Portfolios { get; set; }

    public virtual DbSet<PortfolioPhoto> PortfolioPhotos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HJQSIEA;Database=phalconsoft;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<NewsletterSubscriber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("newsletter_subscriber");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(45);
        });

        modelBuilder.Entity<Portfolio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("portfolio");

            entity.HasIndex(e => e.Category, "Category");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Category).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.PortfolioTitle).HasMaxLength(45);
            entity.Property(e => e.ProjectUrl).HasMaxLength(45);
            entity.Property(e => e.SeoDescription).HasMaxLength(45);
            entity.Property(e => e.SeoUrl).HasMaxLength(45);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Portfolios)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("portfolio_ibfk_1");
        });

        modelBuilder.Entity<PortfolioPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("portfolio_photo");

            entity.HasIndex(e => e.Portfolio, "Portfolio");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CoverPhoto).HasMaxLength(45);
            entity.Property(e => e.Portfolio).HasColumnType("int(11)");
            entity.Property(e => e.ThumbnailPhoto).HasMaxLength(45);

            entity.HasOne(d => d.PortfolioNavigation).WithMany(p => p.PortfolioPhotos)
                .HasForeignKey(d => d.Portfolio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("portfolio_photo_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.PasswordHash).HasColumnType("blob");
            entity.Property(e => e.PasswordSalt).HasColumnType("blob");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
