using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

namespace Game.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        // voice commands
        private GrammarRecognizer _grammarRecognizer;
        private string _spokenWord = "";

        private void Awake()
        {
            // turn cursor on
            Cursor.visible = true;
            
            // turn volume & time back on from in-game menus interaction
            AudioListener.volume = 1f;
            Time.timeScale = 1f;
            
            // reset spoken word
            _spokenWord = "";

            // load in grammar xml file
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "MainMenuControls.xml"), ConfidenceLevel.Low);
            // start grammar recogniser
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            Debug.Log("Menu Voice Controls loaded...");
        }

        private void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
        {
            var message = new StringBuilder();
            // read the semantic meanings from the args passed in.
            var meanings = args.semanticMeanings;
            // for each to get all the meanings.
            foreach (var meaning in meanings)
            {
                // get the items for xml file
                var item = meaning.values[0].Trim();
                message.Append("Words detected: " + item);
                // for calling in VoiceCommands
                _spokenWord = item;
            }

            // print word spoken by user
            Debug.Log(message);
        }

        private void Update()
        {
            // call command for menu controls
            VoiceCommands();
        }

        // VoiceCommands - to call functions for menu controls
        private void VoiceCommands()
        {
            switch (_spokenWord)
            {
                // start items
                case "start game":
                case "play game":
                case "begin game":
                case "continue game":
                    StartGame();
                    break;
                // exit items
                case "exit game":
                case "quit game":
                case "close game":
                    ExitGame();
                    break;
            }
        }

        public void StartGame()
        {
            // start the game
            StartCoroutine(PlayGame());
        }
        
        public void Controls()
        {
            // to controls menu
            StartCoroutine(NextScene());
        }

        public void ExitGame()
        {
            // exit the game
            Application.Quit();
        }

        // fade the music & scene out and load next scene
        private static IEnumerator NextScene()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneChanger.NextScene();
        }
        
        // fade the music & scene out and load park scene
        private static IEnumerator PlayGame()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("ParkScene");
        }

        /// stop the Grammar Recognizer if there is no input
        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}