using Core.Entities;

namespace Entities.Dtos;

public class UserForLoginDto : IDto
{
    public long ChatId { get; set; }
    public string Password { get; set; } = null!;
}
