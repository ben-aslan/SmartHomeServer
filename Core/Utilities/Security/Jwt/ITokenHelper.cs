using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt;

public interface ITokenHelper<TUser, TOperationClaim>
{
    AccessToken CreateToken(TUser user, List<TOperationClaim> operationClaims);
}
