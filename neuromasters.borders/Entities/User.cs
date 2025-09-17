using Microsoft.AspNetCore.Identity;
namespace neuromasters.borders.Entities;
public class User : IdentityUser
{
    public string? Documentos { get; set; }
    public string? DocumentNumber { get; set; }
    public string? FullName { get; set; }
}
