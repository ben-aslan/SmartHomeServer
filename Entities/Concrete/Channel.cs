using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Channel:IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string UserName { get; set; } = null!;

    public long ChatId { get; set; }

    public bool UploadManager { get; set; } = false;
    public bool Active { get; set; } = true;
    public bool Status { get; set; } = true;
}
