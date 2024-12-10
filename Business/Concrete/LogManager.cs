using Business.Abstract;
using DataAccess.Abstract;

namespace Business.Concrete;

public class LogManager : ILogService
{
    ILogDal _logDal;

    public LogManager(ILogDal logDal)
    {
        _logDal = logDal;
    }
}
