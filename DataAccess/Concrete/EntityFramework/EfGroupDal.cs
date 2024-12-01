using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfGroupDal : EfEntityRepositoryBase<Group, EfContext>, IGroupDal
{
}
