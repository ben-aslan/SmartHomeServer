using Entities.Concrete;
using Entities.Enums;

namespace Business.Abstract;

public interface IUserStepService
{
    UserStep Get(long chatId);
    void Set(long chatId, EStep step, EStepIndex stepIndex);
}
