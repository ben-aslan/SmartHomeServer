using Business.Abstract;
using DataAccess.Abstract;

namespace Business.Concrete;

public class StatManager : IStatService
{
    IStatDal _statDal;

    public StatManager(IStatDal statDal)
    {
        _statDal = statDal;
    }
}
