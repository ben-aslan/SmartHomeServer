using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete;

public class UserRole:IEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}
