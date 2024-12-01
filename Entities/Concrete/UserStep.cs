using Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete;

public class UserStep : IEntity
{
    [Key]
    public int Id { get; set; }

    public string? Data { get; set; }

    public bool Status { get; set; } = true;


    [ForeignKey("Step")]
    public int StepId { get; set; }
    public Step Step { get; set; } = null!;

    [ForeignKey("StepIndex")]
    public int StepIndexId { get; set; }
    public StepIndex StepIndex { get; set; } = null!;

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
