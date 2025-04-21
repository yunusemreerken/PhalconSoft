using System.ComponentModel.DataAnnotations;

namespace PhalconSoft.Models;

public class SubscribeViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public byte Active { get; set; } = 1;

}