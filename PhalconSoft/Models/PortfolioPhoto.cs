using System;
using System.Collections.Generic;

namespace PhalconSoft.Models;

public partial class PortfolioPhoto
{
    public int Id { get; set; }

    public string CoverPhoto { get; set; } = null!;

    public string ThumbnailPhoto { get; set; } = null!;

    public int Portfolio { get; set; }

    public virtual Portfolio PortfolioNavigation { get; set; } = null!;
}
