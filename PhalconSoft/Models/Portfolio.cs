using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhalconSoft.Models;

public partial class Portfolio
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Category { get; set; }

    public string SeoUrl { get; set; } = null!;

    public string SeoDescription { get; set; } = null!;

    public string? ProjectUrl { get; set; }


    private DateTime _projectDate;

    [NotMapped]
    public DateOnly ProjectDate
    {
        get => DateOnly.FromDateTime(_projectDate);
        set => _projectDate = value.ToDateTime(new TimeOnly(0, 0));
    }

    public string PortfolioTitle { get; set; } = null!;

    public string PortfolioDetail { get; set; } = null!;

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual ICollection<PortfolioPhoto> PortfolioPhotos { get; } = new List<PortfolioPhoto>();
}
