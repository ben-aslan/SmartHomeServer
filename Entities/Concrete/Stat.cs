using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Stat : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string StatusName { get; set; } = null!;

    [Required]
    public string ValueName { get; set; } = null!;

    public string Value { get; set; } = null!;

    public bool Status { get; set; } = true;
}
