using Business.Abstract;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete;

public class MQTTCredentialManager : IMQTTCredentialService
{
    IMQTTCredentialDal _mqttCredentialDal;

    public MQTTCredentialManager(IMQTTCredentialDal mqttCredentialDal)
    {
        _mqttCredentialDal = mqttCredentialDal;
    }

    public bool Validate(string userName, string password)
    {
        var user = _mqttCredentialDal.GetOrDefault(x => x.UserName == userName && x.Status);
        if (user == null || !HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return false;
        return true;
    }
}
