using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete;

public class UserOperationClaimCore<TOperationClaim, TUser> : IEntity where TOperationClaim : class where TUser : class
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public TUser User { get; set; } = null!;

    [ForeignKey("OperationClaim")]
    public int OperationClaimId { get; set; }
    public TOperationClaim OperationClaim { get; set; } = null!;
}
