using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserStepDal : EfEntityRepositoryBase<UserStep, EfContext>, IUserStepDal
{
}
