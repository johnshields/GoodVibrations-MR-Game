using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
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
            // turn volume back up from pause menu interaction
            AudioListener.volume = 1f;
            _spokenWord = "";

            // load in grammar xml file
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "MenuControls.xml"), ConfidenceLevel.Low);
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
            StartCoroutine(NextScene());
        }

        public void ExitGame()
        {
            // exit the game
            Application.Quit();
        }

        // fade the scene & music out and load next scene
        private static IEnumerator NextScene()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneChanger.NextScene();
        }

        // stop the Grammar Recognizer if game is not running
        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}