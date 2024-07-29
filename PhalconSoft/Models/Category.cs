using System;
using System.Collections.Generic;

namespace PhalconSoft.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Portfolio> Portfolios { get; } = new List<Portfolio>();
}
