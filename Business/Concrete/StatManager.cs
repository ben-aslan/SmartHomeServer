using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class StatManager : IStatService
{
    IStatDal _statDal;

    public StatManager(IStatDal statDal)
    {
        _statDal = statDal;
    }

    public bool Any(string statName, string valueName, string value)
    {
        return _statDal.Any(x => x.StatusName == statName && x.ValueName == valueName && x.Value == value && x.Status);
    }

    public async Task ChangeStatAsync(string statName, string valueName, string value)
    {
        if (_statDal.Any(x => x.StatusName == statName && x.ValueName == valueName && x.Status))
        {
            var stat = await _statDal.FirstAsync(x => x.StatusName == statName && x.ValueName == valueName && x.Status);
            stat.Value = value;
            await _statDal.UpdateAsync(stat);
            return;
        }
        await _statDal.AddAsync(new Stat { StatusName = statName, ValueName = valueName, Value = value });
    }

    public Stat? FirstOrDefalut(string statName, string valueName)
    {
        return _statDal.FirstOrDefault(x => x.StatusName == statName && x.ValueName == valueName && x.Status);
    }
}
