using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Concrete;

public class UserCore<TUserOperationClaim> : IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [MaxLength(50)]
    public string? LastName { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }

    [MaxLength(100)]
    public string? UserName { get; set; }

    [MaxLength(100)]
    public string? UserTag { get; set; }

    public long ChatId { get; set; }

    public bool IsBot { get; set; }

    public bool IsPremium { get; set; }

    public bool AddedToAttachmentMenu { get; set; }

    public bool CanJoinGroups { get; set; }

    public bool CanReadAllGroupMessages { get; set; }

    public bool SupportsInlineQueries { get; set; }

    public byte[] PasswordSalt { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime? LastUpdate { get; set; }

    public string? PhoneNumber { get; set; }

    public bool Status { get; set; } = true;


    public List<TUserOperationClaim> UserOperationClaims { get; set; } = null!;
}