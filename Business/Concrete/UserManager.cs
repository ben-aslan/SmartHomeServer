using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Enums;

namespace Business.Concrete;

public class UserManager : IUserService
{
    IUserDal _userDal;
    IUserStepDal _userStepDal;
    ITokenHelper<User, OperationClaim> _tokenHelper;
    IUserOperationClaimDal _userOperationClaimDal;

    public UserManager(IUserDal userDal, IUserStepDal userStepDal, ITokenHelper<User, OperationClaim> tokenHelper, IUserOperationClaimDal userOperationClaimDal)
    {
        _userDal = userDal;
        _userStepDal = userStepDal;
        _tokenHelper = tokenHelper;
        _userOperationClaimDal = userOperationClaimDal;
    }

    public IResult Add(User user)
    {
        if (_userDal.Any(x => x.ChatId == user.ChatId))
        {
            var userStep = _userStepDal.Get(x => x.User.ChatId == user.ChatId);
            userStep.StepId = (int)EStep.Home;
            userStep.StepIndexId = (int)EStepIndex.Home;
            _userStepDal.Update(userStep);
            return new ErrorResult();
        }
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(user.ChatId.ToString(), out passwordHash, out passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        _userDal.Add(user);
        var tasks = new List<Task>();
        tasks.Add(_userStepDal.AddAsync(new UserStep
        {
            StepId = (int)EStep.Home,
            StepIndexId = (int)EStepIndex.Home,
            UserId = user.Id
        }));
        tasks.Add(_userOperationClaimDal.AddAsync(new UserOperationClaim
        {
            UserId = user.Id,
            OperationClaimId = (int)EOperationClaim.User
        }));
        Task.WhenAll(tasks).Wait();
        return new SuccessResult();
    }

    public IDataResult<EOperationClaim> GetUserClaim(long chatId)
    {
        var userOperationalClaim = _userOperationClaimDal.GetList(x => x.User.ChatId == chatId).OrderBy(x => x.OperationClaim.Periority).First();
        return new SuccessDataResult<EOperationClaim>((EOperationClaim)userOperationalClaim.OperationClaimId);
    }

    public IDataResult<EOperationClaim> GetUserClaimById(int userId)
    {
        var userOperationalClaim = _userOperationClaimDal.GetList(x => x.UserId == userId).OrderBy(x => x.OperationClaim.Periority).First();
        return new SuccessDataResult<EOperationClaim>((EOperationClaim)userOperationalClaim.OperationClaimId);
    }

    public IDataResult<ELang> GetUserLang(long chatId)
    {
        return new SuccessDataResult<ELang>((ELang)(_userDal.GetOrDefault(x => x.ChatId == chatId)?.LanguageId ?? (int)ELang.EN));
    }

    public bool HaveClaim(long chatId, EOperationClaim claim)
    {
        return _userOperationClaimDal.Any(x => x.User.ChatId == chatId && x.OperationClaimId == (int)claim && x.User.Status);
    }

    public IDataResult<AccessToken> LogIn(UserForLoginDto user)
    {
        var userToCheck = _userDal.Get(x => x.ChatId == user.ChatId);
        if (userToCheck == null)
        {
            return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
        }

        if (!HashingHelper.VerifyPasswordHash(user.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<AccessToken>(Messages.PasswordError);
        }

        var claims = _userDal.GetClaims(userToCheck);
        var accessToken = _tokenHelper.CreateToken(userToCheck, claims);

        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }

    public IResult SetUserLang(ELang lang, long chatId)
    {
        var user = _userDal.Get(x => x.ChatId == chatId);
        user.LanguageId = (int)lang;
        _userDal.Update(user);
        return new SuccessResult();
    }
}
