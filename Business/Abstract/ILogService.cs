using Entities.Concrete;

namespace Business.Abstract;

public interface ILogService
{
    Task AddAsync(Log log);
}
