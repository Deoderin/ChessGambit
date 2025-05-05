using GameElements;

namespace Infrastructure.Services
{
    public interface ISelectionService : IService
    {
        Chess Selected { get; }
        void Set(Chess chess);
        void Clear();
        bool HasSelection { get; }
    }
}