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
 * Pause the game if pause button is pressed plus buttons to resume game and go back to Main Menu.
*/
namespace Game.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        // voice commands
        private GrammarRecognizer _grammarRecognizer;
        private string _outAction = "";

        // Pause Menu & Player
        public GameObject pauseMenu;
        private bool _paused;
        private GameObject _dog;

        private void Start()
        {
            // find Player, turn off cursor & set pause menu to false
            _dog = GameObject.Find("Player");
            Cursor.visible = false;
            pauseMenu.SetActive(false);
        }

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

        private void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
        {
            var message = new StringBuilder();
            // read the semantic meanings from the args passed in
            var meanings = args.semanticMeanings;
            // for each to get all the meanings
            foreach (var meaning in meanings)
            {
                // get the items for xml file
                var item = meaning.values[0].Trim();
                message.Append("Out Action: " + item);
                // for calling in VoiceCommands
                _outAction = item;
            }

            print(message);
        }

        private void Update()
        {
            VoiceCommands();

            // pause the game with Esc key
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

        private void PauseGame()
        {
            // for disabling mouse player movement
            _dog.GetComponent<DogController>().enabled = false;
            // pause game
            pauseMenu.SetActive(true);
            Cursor.visible = true; // turn on cursor
            Time.timeScale = 0f; // stop time
            _paused = true; // game is paused
        }

        public void ResumeGame()
        {
            _outAction = "";
            // for enabling mouse player movement
            _dog.GetComponent<DogController>().enabled = true;
            // resume game
            pauseMenu.SetActive(false);
            Cursor.visible = false; // turn off cursor
            Time.timeScale = 1f; // resume time
            _paused = false; // game is unpaused
        }

        public void BackMainMenu()
        {
            // to the main menu
            StartCoroutine(FadeOutMainMenu());
            Time.timeScale = 1f;
        }

        // fade the music & scene out to main menu
        private static IEnumerator FadeOutMainMenu()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}