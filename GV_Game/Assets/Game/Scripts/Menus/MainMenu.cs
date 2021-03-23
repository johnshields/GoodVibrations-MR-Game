using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

/*
 * John Shields - G00348436
 * MainMenu
 * 
 * For UI Buttons and Voice Commands to Start and Exit the Game.
 * Plus UI Button to go to Controls Menu.
*/
namespace Game.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        // GrammarRecognizer and string for Voice Commands.
        private GrammarRecognizer _grammarRecognizer;
        private string _outAction = "";

        // Enable Cursor, set time & volume and start Grammar Recognizer.
        private void Awake()
        {
            // turn cursor, volume and time back on from pause menu interaction
            Cursor.visible = true;
            AudioListener.volume = 1f;
            Time.timeScale = 1f;

            // reset out action
            _outAction = "";
            // Load in grammar xml file.
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "MainMenuControls.xml"), ConfidenceLevel.Low);
            // Start grammar recogniser.
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            print("[INFO] Menu Voice Controls loaded...");
        }

        // Gets phases from MainMenuControls.xml and matches them to User input.
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
                // For calling in Update.
                _outAction = item;
            }

            // print out action detected
            print(message);
        }

        // Call functions with voice commands for menu controls.
        private void Update()
        {
            switch (_outAction)
            {
                // start rule
                case "start the game":
                    StartGame();
                    break;
                // exit rule
                case "exit the game":
                    ExitGame();
                    break;
            }
        }


        // Menu buttons/commands for menu controls.
        public void StartGame()
        {
            StartCoroutine(PlayGame());
        }

        public void Controls()
        {
            StartCoroutine(NextScene());
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        // Fade the music & scene out and load next scene.
        private static IEnumerator NextScene()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneChanger.NextScene();
        }

        // Fade the music & scene out and load park scene.
        private static IEnumerator PlayGame()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("ParkScene");
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