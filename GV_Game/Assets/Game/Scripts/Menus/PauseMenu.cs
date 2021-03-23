using System.Collections;
using System.IO;
using System.Text;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

/*
 * John Shields - G00348436
 * PauseMenu
 * 
 * Pause the game if pause button (Esc Key) is pressed.
 * Plus voice commands & buttons to resume game and go back to Main Menu.
*/
namespace Game.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        // GrammarRecognizer & string for Voice Commands.
        private GrammarRecognizer _grammarRecognizer;
        private string _outAction = "";

        // Pause Menu & Player
        public GameObject pauseMenu;
        private bool _paused;
        private GameObject _dog;

        // Find Player, turn off cursor & set pause menu to false
        private void Start()
        {
            _dog = GameObject.Find("Player");
            Cursor.visible = false;
            pauseMenu.SetActive(false);
        }

        // Start Grammar Recognizer on Awake.
        private void Awake()
        {
            // reset out action
            _outAction = "";
            // load in grammar xml file
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "PauseGameControls.xml"), ConfidenceLevel.Low);
            // start grammar recogniser
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            print("[INFO] Pause Menu Voice Controls loaded...");
        }

        // Gets phases from PauseGameControls.xml and matches them to User input.
        private void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
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

        // Call in VoiceCommands & pause the game with Esc key.
        // Set bool for when is game is pause of unpaused.
        private void Update()
        {
            VoiceCommands();

            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (_paused)
            {
                ResumeGame();
                AudioListener.volume = 1f;
            }
            else
            {
                PauseGame();
                AudioListener.volume = 0f;
            }
        }

        // VoiceCommands - to call functions for Pause Menu Options.
        private void VoiceCommands()
        {
            switch (_outAction)
            {
                // resume game rule
                case "resume the game":
                    ResumeGame();
                    break;
                // back to main menu rule
                case "back to main menu":
                    BackMainMenu();
                    break;
            }
        }

        // Pause the game and disable mouse player movement.
        private void PauseGame()
        {
            // Reset out action in case anything was picked up in gameplay.
            _outAction = "";
            _dog.GetComponent<DogController>().enabled = false;
            Cursor.visible = true; // turn on cursor
            pauseMenu.SetActive(true); // pause game
            Time.timeScale = 0f; // stop time
            _paused = true; // game is paused
        }

        // Resume Game and enable mouse player movement.
        public void ResumeGame()
        {
            // To stop voice commands activating function in gameplay.
            if (!_paused) return;
            // Reset out action to stop pause menu flickering when pressing Esc key.
            _outAction = "";
            _dog.GetComponent<DogController>().enabled = true;
            Cursor.visible = false; // turn off cursor
            pauseMenu.SetActive(false); // resume game
            Time.timeScale = 1f; // resume time
            AudioListener.volume = 1f; // turn audio back on
            _paused = false; // game is unpaused
        }

        // Start a Coroutine to go to main menu.
        public void BackMainMenu()
        {
            if (!_paused) return;
            StartCoroutine(FadeOutMainMenu());
            Time.timeScale = 1f;
        }

        // Fade the music & scene out to main menu.
        private static IEnumerator FadeOutMainMenu()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("MainMenuScene");
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