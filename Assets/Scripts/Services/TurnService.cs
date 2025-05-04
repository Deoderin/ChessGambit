namespace Services
{
    public class TurnService : ITurnService
    {
        private bool _isPlayerTurn = true;

        public bool IsPlayerTurn => _isPlayerTurn;
        public bool IsEnemyTurn => !_isPlayerTurn;

        public void EndTurn()
        {
            _isPlayerTurn = !_isPlayerTurn;
        }

        public void Reset()
        {
            _isPlayerTurn = true;
        }
    }
}