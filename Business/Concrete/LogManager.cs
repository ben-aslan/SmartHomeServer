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

    public async Task AddAsync(Log log)
    {
        await _logDal.AddAsync(log);
    }
}
