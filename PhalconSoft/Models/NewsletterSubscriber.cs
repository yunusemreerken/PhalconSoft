using System;
using System.Collections.Generic;

namespace PhalconSoft.Models;

public partial class NewsletterSubscriber
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public bool Active { get; set; }
}
