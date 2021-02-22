using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

namespace Game.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        // voice commands
        private GrammarRecognizer _grammarRecognizer;
        private static string _spokenWord = "";

        // pause menu
        public GameObject pauseMenu;
        private bool _paused;

        private void Start()
        {
            // set pause menu to false
            pauseMenu.SetActive(false);

            // load in grammar xml file
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "MenuControls.xml"), ConfidenceLevel.Low);
            // start grammar recogniser
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            Debug.Log("Pause Menu Voice Controls loaded...");
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

        // VoiceCommands - to call functions for menu controls
        private void VoiceCommands()
        {
            switch (_spokenWord)
            {
                case "pause game":
                case "hold game":
                    PauseGame();
                    break;
                case "resume game":
                case "back to game":
                    ResumeGame();
                    break;
                case "main menu":
                case "go home":
                    BackMainMenu();
                    break;
            }
        }

        private void PauseGame()
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.volume = 0f; // pause audio
            _paused = true; // game is paused
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _paused = false;
            AudioListener.volume = 1f;
        }

        public void BackMainMenu()
        {
            // to the main menu
            StartCoroutine(FadeOutMainMenu());
            Time.timeScale = 1f;
        }

        private static IEnumerator FadeOutMainMenu()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("MainMenuScene");
        }


        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}