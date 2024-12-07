using Business.Abstract;
using DataAccess.Abstract;

namespace Business.Concrete;

public class MQTTCredentialManager : IMQTTCredentialService
{
    IMQTTCredentialDal _mqttCredentialDal;

    public MQTTCredentialManager(IMQTTCredentialDal mqttCredentialDal)
    {
        _mqttCredentialDal = mqttCredentialDal;
    }
}
