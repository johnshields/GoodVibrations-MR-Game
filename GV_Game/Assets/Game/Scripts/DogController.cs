using UnityEngine;

namespace Game.Scripts
{
    public class DogController : MonoBehaviour
    {
        // dog stats
        private float _currentProfile;
        [SerializeField] public float lowProfile = 0.02f;
        [SerializeField] public float highProfile = 0.06f;
        [SerializeField] public float rotationSpeed = 4.0f;
        private Rigidbody _bodyPhysics;
        private Animator _animator;
        [SerializeField] public float jumpHeight = 20f;
        [SerializeField] public float paws = 20f;
        [SerializeField] public AudioClip bark;

        // Camera 
        public Transform cameraTransform;
        private float _yaw;
        private float _pitch;

        // animator booleans
        private int _idleActive;
        private int _walkActive;
        private int _runActive;
        private int _barkActive;
        private int _jumpActive;

        private void Start()
        {
            _bodyPhysics = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            // low profile
            _idleActive = Animator.StringToHash("IdleActive");
            _walkActive = Animator.StringToHash("WalkActive");
            // high profile
            _runActive = Animator.StringToHash("RunActive");
            _barkActive = Animator.StringToHash("BarkActive");
            _jumpActive = Animator.StringToHash("JumpActive");
        }

        private void FixedUpdate()
        {
            DogMovement();
            Jump();
            Bark();
            CameraMovement();
        }

        private void DogMovement()
        {
            // move dog
            var z = Input.GetAxis("Vertical") * _currentProfile;
            var y = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Translate(0, 0, z);
            transform.Rotate(0, y, 0);

            // player Inputs
            var forwardPressed = Input.GetKey("w");
            var highProfilePressed = Input.GetKey("left shift");

            if (highProfilePressed)
            {
                if (forwardPressed)
                {
                    // Run
                    _animator.SetBool(_runActive, true);
                    _animator.SetBool(_idleActive, false);
                    _animator.SetBool(_walkActive, false);
                    _animator.SetBool(_barkActive, false);
                    _animator.SetBool(_jumpActive, false);
                }
                else
                {
                    // Idle
                    _animator.SetBool(_idleActive, true);
                    _animator.SetBool(_runActive, false);
                    _animator.SetBool(_walkActive, false);
                    _animator.SetBool(_barkActive, false);
                    _animator.SetBool(_jumpActive, false);
                }

                _currentProfile = highProfile;
            }
            else
            {
                if (forwardPressed)
                {
                    // Walk
                    _animator.SetBool(_walkActive, true);
                    _animator.SetBool(_runActive, false);
                    _animator.SetBool(_idleActive, false);
                    _animator.SetBool(_barkActive, false);
                    _animator.SetBool(_jumpActive, false);
                }
                else
                {
                    // Idle
                    _animator.SetBool(_idleActive, true);
                    _animator.SetBool(_runActive, false);
                    _animator.SetBool(_walkActive, false);
                    _animator.SetBool(_barkActive, false);
                    _animator.SetBool(_jumpActive, false);
                }

                _currentProfile = lowProfile;
            }
        }

        private void Jump()
        {
            if (!Input.GetButtonDown("Jump")) return;
            // player does not jump if is not touching the ground layer
            if (!(transform.position.y <= paws)) return;
            _bodyPhysics.AddForce(Vector3.up * jumpHeight);
            _animator.SetBool(_jumpActive, true);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_runActive, false);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_barkActive, false);
        }

        private void Bark()
        {
            // Player Input
            var barkPressed = Input.GetKeyDown("b");
            // Animator bool
            var barkActive = _animator.GetBool(_barkActive);

            if (barkPressed)
            {
                // bark
                AudioSource.PlayClipAtPoint(bark, transform.position);
                _animator.SetBool(_barkActive, true);
                _animator.SetBool(_idleActive, false);
                _animator.SetBool(_runActive, false);
                _animator.SetBool(_walkActive, false);
                _animator.SetBool(_jumpActive, false);
            }

            // idle
            if (!barkActive || barkPressed) return;
            _animator.SetBool(_idleActive, true);
            _animator.SetBool(_barkActive, false);
            _animator.SetBool(_runActive, false);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_jumpActive, false);
        }


        private void CameraMovement()
        {
            // move Camera with mouse
            _yaw += rotationSpeed * Input.GetAxisRaw("Mouse X");
            _pitch -= rotationSpeed * Input.GetAxisRaw("Mouse Y");
            transform.eulerAngles = new Vector3(0, _yaw, 0);
            cameraTransform.eulerAngles = new Vector3(_pitch, _yaw, 0);
        }
    }
}