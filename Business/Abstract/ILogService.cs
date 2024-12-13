using Entities.Concrete;

namespace Business.Abstract;

public interface ILogService
{
    Task AddAsync(string name, string value, string device);
}
