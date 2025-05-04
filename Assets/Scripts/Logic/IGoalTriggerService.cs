using Infrastructure.Services;
using Logic.Goal;

namespace Logic
{
    public interface IGoalTriggerService : IService
    {
        void RegisterTrigger(LevelGoalTrigger trigger);
        void ClearTriggers();
        void CheckAllTriggers();
    }
}