using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Enums;

namespace Business.Abstract;

public interface IUserService
{
    IResult Add(User user);
    IDataResult<AccessToken> LogIn(UserForLoginDto user);
    IDataResult<ELang> GetUserLang(long chatId);
    IResult SetUserLang(ELang lang, long chatId);
    IDataResult<EOperationClaim> GetUserClaim(long chatId);
    IDataResult<EOperationClaim> GetUserClaimById(int userId);
    bool HaveClaim(long chatId, EOperationClaim claim);
}
