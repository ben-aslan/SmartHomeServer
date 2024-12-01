using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete;

public class Photo:IEntity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(500)]
    public string FileId { get; set; } = null!;

    [MaxLength(500)]
    public string FileUniqueId { get; set; } = null!;

    public int Height { get; set; }

    public int Width { get; set; }

    public int FileSize { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;
}
