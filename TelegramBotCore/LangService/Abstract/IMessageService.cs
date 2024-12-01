using Entities.Enums;

namespace Business.LangService.Abstract;

public interface IMessageService
{
    string Get(EMessage message);
    Task<string> GetAsync(EMessage message);
    string GetByName(string name);
    Task<string> GetByNameAsync(string name);
}
