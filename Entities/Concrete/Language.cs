using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Language : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string LangCode { get; set; } = null!;

    public List<User> Users { get; set; } = null!;
}
