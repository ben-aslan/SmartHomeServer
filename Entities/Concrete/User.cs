using Core.Entities;
using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete;

public class User : UserCore<UserOperationClaim>, IEntity
{

    [ForeignKey("Language")]
    public int? LanguageId { get; set; }
    public Language Language { get; set; } = null!;


    public List<UserStep> UserSteps { get; set; } = null!;
    public List<UserRole> UserRoles { get; set; } = null!;
}
