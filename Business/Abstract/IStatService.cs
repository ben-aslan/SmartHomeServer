using Entities.Concrete;

namespace Business.Abstract;

public interface IStatService
{
    Task ChangeStatAsync(string statName, string valueName, string value);
    Stat? FirstOrDefalut(string statName, string valueName);
}
