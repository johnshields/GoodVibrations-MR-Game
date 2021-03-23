using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

/*
 * John Shields - G00348436
 * DogController
 * 
 * For Controlling the Main Dog Character by Voice Commands, Keyboard and Mouse.
*/
namespace Game.Scripts.Player
{
    public class DogController : MonoBehaviour
    {
        // GrammarRecognizer & string for Voice Commands.
        private GrammarRecognizer _grammarRecognizer;
        private static string _outAction = "";

        // Dog stats for movement.
        [SerializeField] public float lowProfile = 1f;
        [SerializeField] public float highProfile = 3f;
        [SerializeField] public float rotationSpeed = 4.0f;
        [SerializeField] public float jumpForce = 2.0f;
        private bool _grounded;

        // Dog components.
        private Rigidbody _bodyPhysics;
        private Animator _animator;

        // For animator booleans.
        private int _idleActive;
        private int _walkActive;
        private int _runActive;
        private int _jumpActive;
        private int _sitActive;

        // For camera movement with mouse.
        public Transform cameraTransform;
        private float _yaw;
        private float _pitch;

        // Start Grammar Recognizer, find necessary components and animations.
        private void Awake()
        {
            // reset out action
            _outAction = "";
            // Load in grammar xml file.
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "DogControls.xml"), ConfidenceLevel.Low);
            // Start grammar recogniser.
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            print("[INFO] Player Voice Controls loaded...");

            // For enabling mouse player movement when loaded back from the
            // main menu after going back to main menu from pause menu.
            GetComponent<DogController>().enabled = true;

            // for jumping
            _bodyPhysics = GetComponent<Rigidbody>();

            // Dog animator and Hash ints to get animator booleans.
            _animator = GetComponent<Animator>();
            _idleActive = Animator.StringToHash("IdleActive");
            _walkActive = Animator.StringToHash("WalkActive");
            _sitActive = Animator.StringToHash("SitActive");
            _runActive = Animator.StringToHash("RunActive");
            _jumpActive = Animator.StringToHash("JumpActive");
        }

        // Gets phases from DogControls.xml and matches them to User input.
        private static void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
        {
            var message = new StringBuilder();
            // Read the semantic meanings from the args passed in.
            var meanings = args.semanticMeanings;
            // For each to get all the meanings.
            foreach (var meaning in meanings)
            {
                // Get the items for xml file.
                var item = meaning.values[0].Trim();
                message.Append("Out Action: " + item);
                // For calling in VoiceCommands.
                _outAction = item;
            }

            // Print out action detected.
            print(message);
        }

        // Call necessary commands for movement.
        private void Update()
        {
            VoiceCommands();
            Jump();
            CameraMovement();
        }

        // VoiceCommands - to call functions for dog movement.
        private void VoiceCommands()
        {
            switch (_outAction)
            {
                // idle rule
                case "be idle dog":
                    Idle();
                    break;
                // sit rule
                case "sit dog":
                    Sit();
                    break;
                // walk rule
                case "walk dog":
                    Walk();
                    break;
                // run rule 
                case "run dog":
                    Run();
                    break;
            }
        }

        // Stop dog and idle animation.
        private void Idle()
        {
            transform.position += new Vector3(0, 0, 0);
            _animator.SetBool(_idleActive, true);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_runActive, false);
            _animator.SetBool(_sitActive, false);
        }

        // Move dog (in a low profile), mouse rotation and walk animation.
        private void Walk()
        {
            transform.Translate(0, 0, lowProfile * Time.deltaTime);
            var y = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Rotate(0, y, 0);
            _animator.SetBool(_walkActive, true);
            _animator.SetBool(_runActive, false);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_sitActive, false);
        }

        // Move dog (in a high profile), mouse rotation and run animation.
        private void Run()
        {
            transform.Translate(0, 0, highProfile * Time.deltaTime);
            var y = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Rotate(0, y, 0);
            _animator.SetBool(_runActive, true);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_sitActive, false);
        }

        // Anything the dog collides with make the dog grounded.
        private void OnCollisionEnter()
        {
            _grounded = true;
        }

        // Jump if space bar is pressed, dog is grounded and not sitting & jump animation.
        private void Jump()
        {
            var sitActive = _animator.GetBool(_sitActive);

            if (Input.GetKeyDown(KeyCode.Space) && _grounded && !sitActive)
            {
                _bodyPhysics.velocity = transform.TransformDirection(0, jumpForce, 1);
                _grounded = false;
                _animator.SetBool(_jumpActive, true);
                _animator.SetBool(_sitActive, false);
                _animator.SetBool(_runActive, false);
                _animator.SetBool(_walkActive, false);
                _animator.SetBool(_idleActive, false);
            }
            else
            {
                _animator.SetBool(_jumpActive, false);
            }
        }

        // Stop dog and sit animation.
        private void Sit()
        {
            transform.position += new Vector3(0, 0, 0);
            _animator.SetBool(_sitActive, true);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_runActive, false);
        }

        // Move camera with mouse to rotate dog in desired direction.
        private void CameraMovement()
        {
            _yaw += rotationSpeed * Input.GetAxisRaw("Mouse X");
            _pitch -= rotationSpeed * Input.GetAxisRaw("Mouse Y");
            transform.eulerAngles = new Vector3(0, _yaw, 0);
            cameraTransform.eulerAngles = new Vector3(_pitch, _yaw, 0);
        }

        // Stop the Grammar Recognizer if there is no input.
        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}