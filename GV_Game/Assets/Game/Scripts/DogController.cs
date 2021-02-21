using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Game.Scripts
{
    public class DogController : MonoBehaviour
    {
        // voice commands
        private GrammarRecognizer _grammarRecognizer;
        private static string _spokenWord = "";
        
        // dog stats
        private Animator _animator;
        [SerializeField] public float lowProfile = 2.0f;
        [SerializeField] public float highProfile = 6.0f;
        [SerializeField] public float rotationSpeed = 4.0f;
        [SerializeField] public AudioClip bark;
        
        // animator booleans
        private int _idleActive;
        private int _walkActive;
        private int _runActive;

        // for camera movement 
        public Transform cameraTransform;
        private float _yaw;
        private float _pitch;
        
        private void Start()
        {
            // load in grammar xml file
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, 
                "DogControls.xml"), ConfidenceLevel.Low);
            // start grammar recogniser
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            Debug.Log("Player Voice Controls loaded...");
            
            // dog animator
            _animator = GetComponent<Animator>();
            // low profile
            _idleActive = Animator.StringToHash("IdleActive");
            _walkActive = Animator.StringToHash("WalkActive");
            // high profile
            _runActive = Animator.StringToHash("RunActive");
        }

        private static void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
        {
            var message = new StringBuilder();
            // read the semantic meanings from the args passed in.
            var meanings = args.semanticMeanings;
            // for each to get all the meanings.
            foreach (var meaning in meanings)
            {
                // get the items for xml file
                var item = meaning.values[0].Trim();
                message.Append("Word detected: " + item);
                // for calling in VoiceCommands
                _spokenWord = item;
            }
            // print word spoken by user
            Debug.Log(message);
        }
        
        private void Update()
        {
            VoiceCommands();
            CameraMovement();
        }

        // VoiceCommands - to call functions for dog movement
        private void VoiceCommands()
        {
            switch (_spokenWord)
            {
                case "idle":
                case "yield":
                case "stop":
                    Idle();
                    break;
                case "walk":
                case "go":
                case "forward":
                    Walk();
                    break;
                case "run":
                case "sprint":
                case "faster":
                    Run();
                    break;
            }
        }
        
        private void Idle()
        {
            // stop dog
            transform.position += new Vector3(0,0,0);
            // idle animation
            _animator.SetBool(_idleActive, true);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_runActive, false);
        }
        
        private void Walk()
        {
            // move dog
            transform.Translate(0, 0, lowProfile*Time.deltaTime);
            var y = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Rotate(0, y, 0);
            // walk animation
            _animator.SetBool(_walkActive, true);
            _animator.SetBool(_runActive, false);
            _animator.SetBool(_idleActive, false);
        }
        
        private void Run()
        {
            // move dog
            transform.Translate(0, 0, highProfile*Time.deltaTime);
            var y = Input.GetAxis("Horizontal") * rotationSpeed;
            transform.Rotate(0, y, 0);
            // run animation
            _animator.SetBool(_runActive, true);
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_idleActive, false);
        }

        private void CameraMovement()
        {
            // move camera with mouse
            _yaw += rotationSpeed * Input.GetAxisRaw("Mouse X");
            _pitch -= rotationSpeed * Input.GetAxisRaw("Mouse Y");
            transform.eulerAngles = new Vector3(0, _yaw, 0);
            cameraTransform.eulerAngles = new Vector3(_pitch, _yaw, 0);
        }

        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}
