using GameElements;

namespace Infrastructure.Services
{
    public class SelectionService : ISelectionService
    {
        public Chess Selected { get; private set; }

        public bool HasSelection => Selected != null;

        public void Set(Chess chess)
        {
            Selected = chess;
        }

        public void Clear()
        {
            Selected = null;
        }
    }
}