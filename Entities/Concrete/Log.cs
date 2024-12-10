using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Log : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string vlaue { get; set; } = null!;

    public DateTime CreateDate { get; set; } = DateTime.Now;

    public bool Status { get; set; } = true;
}
