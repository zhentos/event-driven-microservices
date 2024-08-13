using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public abstract class Entity
{
    [Key]
    public Guid Id { get; set; }
}
