using Core.Entities;
using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete;

public class MQTTCredentialOperationClaim : IEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("MQTTCredential")]
    public int MQTTCredentialId { get; set; }
    public MQTTCredential MQTTCredential { get; set; } = null!;

    [ForeignKey("OperationClaim")]
    public int OperationClaimId { get; set; }
    public OperationClaim OperationClaim { get; set; } = null!;
}
