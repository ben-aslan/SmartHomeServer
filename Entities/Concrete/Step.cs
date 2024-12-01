using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Step : IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool Status { get; set; } = true;

    public List<UserStep> UserSteps { get; set; } = null!;
}
