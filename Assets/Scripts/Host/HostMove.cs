using Data;
using Infrastructure.Services;
using Services;
using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Host{
    public class HostMove : MonoBehaviour, ISaveProgress{
        [SerializeField] private CharacterController characterController;
        [SerializeField] private new Camera camera;
        [SerializeField] private float movementSpeed;

        public float sensitivity;

        private IInputService _inputService;
        private Vector3 _rotation;
        private float _jumpForce;
        private float _gravityForce;

        private void Awake(){
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update(){
            Rotation();
            Move();
        }

        private void Move(){
            Vector3 movementVector = Vector3.zero;

            if(_inputService.Axis.magnitude > 0.1f){
                movementVector = camera.transform.TransformDirection(_inputService.Axis);
                movementVector.Normalize();
            }

            movementVector.y += JumpForce();
            movementVector.y += GravityAcceleration();
            characterController.Move(movementVector * (movementSpeed * Time.deltaTime));
        }

        private void Rotation(){
            const float headMinY = -90;
            const float headMaxY = 90;

            _rotation.x = transform.localEulerAngles.y + _inputService.Direction.x;
            _rotation.y += _inputService.Direction.y;
            _rotation.y = Mathf.Clamp(_rotation.y, headMinY, headMaxY);
            _rotation *= sensitivity;

            transform.localEulerAngles = new Vector3(-_rotation.y, _rotation.x, 0);
        }

        private float JumpForce(){
            const float jumpTime = 10;
            const float jumpForce = 4;

            bool isJump = _inputService.IsJumpButton() && characterController.isGrounded;

            if(_jumpForce > 0){
                _jumpForce -= Time.deltaTime * jumpTime;
                return _jumpForce;
            }

            if(isJump)
                _jumpForce = jumpForce;

            return 0;
        }

        private float GravityAcceleration(){
            const float minGravityForce = -1.5f;
            const float maxGravityForce = -5;

            if(characterController.isGrounded)
                return _gravityForce = minGravityForce;

            _gravityForce -= Time.deltaTime;

            if(_gravityForce <= maxGravityForce)
                return _gravityForce = maxGravityForce;

            return _gravityForce;
        }

        public void LoadProgress(PlayerProgress _progress){
            var position = _progress.worldData.PositionOnLevel.position;
        }

        public void UpdateProgress(PlayerProgress _progress){
            _progress.worldData.PositionOnLevel
                = new PositionOnLevel(SceneManager.GetActiveScene().name, transform.position.AsVectorData());
        }
    }
}
