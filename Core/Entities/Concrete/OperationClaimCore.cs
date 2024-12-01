using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete;

public class OperationClaimCore<TUserOperationClaim> : IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(200)]
    public string Name { get; set; } = null!;

    public int Periority { get; set; } = 1;

    public List<TUserOperationClaim>? UserOperationClaims { get; set; }
}
