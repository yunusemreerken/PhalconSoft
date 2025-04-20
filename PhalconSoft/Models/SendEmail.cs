using System.ComponentModel.DataAnnotations;

namespace PhalconSoft.Models;

public class SendEmail
{
    [Key]
    public int Id { get; set; }
    public string? NameSurname { get; set; }
    public string? Email { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
}