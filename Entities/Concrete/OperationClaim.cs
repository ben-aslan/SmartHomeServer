using Core.Entities;
using Core.Entities.Concrete;

namespace Entities.Concrete;

public class OperationClaim : OperationClaimCore<UserOperationClaim>, IEntity
{
    public List<MQTTCredentialOperationClaim> MQTTCredentialOperationClaims { get; set; } = null!;
}
