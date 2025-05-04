using Infrastructure.Services;

namespace Services
{
    public interface ITurnService : IService
    {
        bool IsPlayerTurn { get; }
        bool IsEnemyTurn { get; }

        void EndTurn();
        void Reset();
    }
}