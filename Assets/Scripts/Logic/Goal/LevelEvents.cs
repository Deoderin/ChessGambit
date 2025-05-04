using System;
using System.Collections.Generic;

namespace Logic.Goal
{
    public static class LevelEvents
    {
        public static event Action OnLevelCompleted;

        private static HashSet<KillToComplete> _requiredEnemies = new();

        public static void RegisterRequiredEnemy(KillToComplete enemy)
        {
            _requiredEnemies.Add(enemy);
        }

        public static void RegisterEnemyKilled(KillToComplete enemy)
        {
            _requiredEnemies.Remove(enemy);
            if (_requiredEnemies.Count == 0)
                RaiseOnLevelCompleted();
        }

        public static void RaiseOnLevelCompleted()
        {
            OnLevelCompleted?.Invoke();
        }

        public static void Clear()
        {
            _requiredEnemies.Clear();
        }
    }
}