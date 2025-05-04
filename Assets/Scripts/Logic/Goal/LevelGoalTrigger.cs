using GameElements;
using UnityEngine;

namespace Logic.Goal
{
    public class LevelGoalTrigger : MonoBehaviour
    {
        public bool Triggered => _triggered;
        private bool _triggered = false;

        public void TryTrigger(Chess chess)
        {
            if (_triggered || chess.Side != ColorSide.White)
                return;

            _triggered = true;
            LevelEvents.RaiseOnLevelCompleted();
        }
    }
}