using GameElements;
using Infrastructure.Services;

namespace Services
{
    public interface ICombatService : IService
    {
        void Attack(Chess attacker, Chess defender);
    }
}