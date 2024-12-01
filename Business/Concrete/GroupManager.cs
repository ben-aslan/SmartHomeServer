using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class GroupManager : IGroupService
{
    IGroupDal _groupDal;

    public GroupManager(IGroupDal groupDal)
    {
        _groupDal = groupDal;
    }

    public Group GetSelectedGroup()
    {
        return _groupDal.Get(x => x.Selected && x.Active && x.Status);
    }
}
