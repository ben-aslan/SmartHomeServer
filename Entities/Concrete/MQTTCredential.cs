using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class MQTTCredential : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string UserName { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public bool Status { get; set; } = true;

    public List<MQTTCredentialOperationClaim> MQTTCredentialOperationClaims { get; set; } = null!;
}
