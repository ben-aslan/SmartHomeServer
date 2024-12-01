using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Role : IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool Status { get; set; } = true;

    public bool AccessToBotSettings { get; set; }
    public bool AccessSendMessageToChannel { get; set; }
    public bool AccessToUsersList { get; set; }
    public bool AccessToStatistics { get; set; }

    public List<UserRole> UserRoles { get; set; } = null!;
}
