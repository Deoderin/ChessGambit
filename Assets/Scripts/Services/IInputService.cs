using Infrastructure.Services;
using UnityEngine;

namespace Services{
    public interface IInputService : IService{
        Vector3 Axis{get;}
        Vector2 Direction{get;}
        bool IsShootButton();
        bool IsShootAlternativeButton();
        bool IsJumpButton();
    }
}
