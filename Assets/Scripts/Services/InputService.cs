using UnityEngine;

namespace Services{
    public class InputService : IInputService{
        private const string FireButton = "Fire1";
        private const string FireButtonAlternative = "Fire2";
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";
        private const string VerticalDirection = "Mouse Y";
        private const string HorizontalDirection = "Mouse X";

        public Vector3 Axis => new(Input.GetAxisRaw(Horizontal), 0, Input.GetAxisRaw(Vertical));
        public Vector2 Direction => new(Input.GetAxis(HorizontalDirection), Input.GetAxis(VerticalDirection));
        public bool IsShootButton() => Input.GetButtonDown(FireButton);
        public bool IsShootAlternativeButton() => Input.GetButtonDown(FireButtonAlternative);
        public bool IsJumpButton() => Input.GetKeyDown(KeyCode.Space);
    }
}