using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserDal : EfEntityRepositoryBase<User, EfContext>, IUserDal
{
    public List<OperationClaim> GetClaims(User user)
    {
        using (var context = new EfContext())
        {
            return context.Users.AsNoTracking().Include(x => x.UserOperationClaims).ThenInclude(x => x.OperationClaim).Where(x => x.Id == user.Id).First().UserOperationClaims.Select(x => x.OperationClaim).ToList()!;
        }
    }
}
