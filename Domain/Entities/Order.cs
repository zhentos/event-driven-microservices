using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Order : Entity
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string? Title { get; set; }
}
