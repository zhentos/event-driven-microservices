using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class User : Entity
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
}
