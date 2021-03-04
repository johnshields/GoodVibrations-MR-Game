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
        // voice commands
        private GrammarRecognizer _grammarRecognizer;
        private static string _spokenWord = "";

        // dog stats
        [SerializeField] public float lowProfile = 1f;
        [SerializeField] public float highProfile = 3f;
        [SerializeField] public float rotationSpeed = 4.0f;
        [SerializeField] public float jumpForce = 2.0f;
        private bool _grounded;
        // dog components
        private Rigidbody _bodyPhysics;
        private Animator _animator;

        // for animator booleans
        private int _idleActive;
        private int _walkActive;
        private int _runActive;
        private int _jumpActive;
        private int _sitActive;

        // for camera movement 
        public Transform cameraTransform;
        private float _yaw;
        private float _pitch;

        private void Awake()
        {
            // reset the spoken word to nothing
            _spokenWord = "";

            // load in grammar xml file
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "DogControls.xml"), ConfidenceLevel.Low);
            // start grammar recogniser
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            print("[INFO] Player Voice Controls loaded...");
            
            // for enabling mouse player movement when loaded back from the
            // main menu after going back to main menu from pause menu
            GetComponent<DogController>().enabled = true;
            
            // for jumping
            _bodyPhysics = GetComponent<Rigidbody>();

            // dog animator
            _animator = GetComponent<Animator>();
            // hash ints to get animator booleans
            _idleActive = Animator.StringToHash("IdleActive");
            _walkActive = Animator.StringToHash("WalkActive");
            _sitActive  = Animator.StringToHash("SitActive");
            _runActive = Animator.StringToHash("RunActive");
            _jumpActive  = Animator.StringToHash("JumpActive");
        }

        private static void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
        {
            var message = new StringBuilder();
            // read the semantic meanings from the args passed in
            var meanings = args.semanticMeanings;
            // for each to get all the meanings
            foreach (var meaning in meanings)
            {
                // get the items for xml file
                var item = meaning.values[0].Trim();
                message.Append("[INFO] Words detected: " + item);
                // for calling in VoiceCommands
                _spokenWord = item;
            }

            // print word spoken by user
            print(message);
        }

        private void Update()
        {
            // call necessary commands for movement
            VoiceCommands();
            Jump();
            CameraMovement();
        }

        // VoiceCommands - to call functions for dog movement
        private void VoiceCommands()
        {
            switch (_spokenWord)
            {
                // idle items
                case "idle dog":
                case "yield dog":
                case "stop dog":
                case "halt dog":
                    Idle();
                    break;
                // sit items
                case "sit dog":
                case "rest dog":
                    Sit();
                    break;
                // walk items
                case "walk dog":
                case "go dog":
                case "stroll dog":
                case "wander dog":
                    Walk();
                    break;
                // run items
                case "run dog":
                case "jog dog":
                case "dash dog":
                    Run();
                    break;
            }
        }

        private void Idle()
        {
            // stop dog & idle animation
            transform.position += new Vector3(0, 0, 0);
            _animator.SetBool(_idleActive, true);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_runActive, false);
            _animator.SetBool(_sitActive, false);
        }

        private void Walk()
        {
            // move dog & walk animation
            transform.Translate(0, 0, lowProfile * Time.deltaTime);
            var y = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Rotate(0, y, 0);
            _animator.SetBool(_walkActive, true);
            _animator.SetBool(_runActive, false);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_sitActive, false);
        }

        private void Run()
        {
            // move dog & run animation
            transform.Translate(0, 0, highProfile * Time.deltaTime);
            var y = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Rotate(0, y, 0);
            _animator.SetBool(_runActive, true);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_sitActive, false);
        }

        // anything the dog collides with make the dog grounded
        private void OnCollisionEnter(){
            _grounded = true;
        }

        private void Jump()
        {
            // jump if dog is grounded & jump animation
            if(Input.GetKeyDown(KeyCode.Space) && _grounded){
                _bodyPhysics.velocity = transform.TransformDirection(0 , jumpForce, 1);
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
        
        private void Sit()
        {
            // stop dog & sit animation
            transform.position += new Vector3(0, 0, 0);
            _animator.SetBool(_sitActive, true);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_runActive, false);
        }

        private void CameraMovement()
        {
            // move camera with mouse
            _yaw += rotationSpeed * Input.GetAxisRaw("Mouse X");
            _pitch -= rotationSpeed * Input.GetAxisRaw("Mouse Y");
            transform.eulerAngles = new Vector3(0, _yaw, 0);
            cameraTransform.eulerAngles = new Vector3(_pitch, _yaw, 0);
        }

        // stop the Grammar Recognizer if there is no input
        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}