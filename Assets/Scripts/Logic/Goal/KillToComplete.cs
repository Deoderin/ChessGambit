using UnityEngine;

namespace Logic.Goal
{
    public class KillToComplete : MonoBehaviour
    {
        private void OnDestroy()
        {
            LevelEvents.RegisterEnemyKilled(this);
        }
    }
}