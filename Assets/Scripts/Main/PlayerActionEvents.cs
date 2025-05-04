using System;

namespace Main
{
    public static class PlayerActionEvents
    {
        public static event Action OnTurnEnded;

        public static void RaiseTurnEnded() => OnTurnEnded?.Invoke();
    }
}