using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Enums;

namespace Business.Concrete;

public class UserStepManager : IUserStepService
{
    IUserStepDal _userStepDal;

    public UserStepManager(IUserStepDal userStepDal)
    {
        _userStepDal = userStepDal;
    }

    public UserStep Get(long chatId)
    {
        return _userStepDal.First(x => x.User.ChatId == chatId);
    }

    public void Set(long chatId, EStep step, EStepIndex stepIndex)
    {
        var userStep = _userStepDal.Get(x => x.User.ChatId == chatId && x.User.Status);
        userStep.StepId = (int)step;
        userStep.StepIndexId = (int)stepIndex;
        _userStepDal.Update(userStep);
    }
}
