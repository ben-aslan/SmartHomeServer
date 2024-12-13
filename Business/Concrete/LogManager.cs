using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete;

public class LogManager : ILogService
{
    ILogDal _logDal;

    public LogManager(ILogDal logDal)
    {
        _logDal = logDal;
    }

    public async Task AddAsync(string name, string value, string device)
    {
        await _logDal.AddAsync(new Log { Name = name, Value = value, Device = device });
    }
}
