namespace Business.Abstract;

public interface IMQTTCredentialService
{
    public bool Validate(string userName, string password);
}
