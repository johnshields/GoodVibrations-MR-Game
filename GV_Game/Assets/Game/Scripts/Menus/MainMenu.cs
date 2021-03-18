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
        // voice commands
        private GrammarRecognizer _grammarRecognizer;
        private string _outAction = "";

        private void Awake()
        {
            // turn cursor, volume & time back on from in-game menus interaction
            Cursor.visible = true;
            AudioListener.volume = 1f;
            Time.timeScale = 1f;
            
            // reset out action as they can carry over from Park Scene
            _outAction = "";

            // load in grammar xml file
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "MainMenuControls.xml"), ConfidenceLevel.Low);
            // start grammar recogniser
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            print("[INFO] Menu Voice Controls loaded...");
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
                message.Append("Phrase detected: " + item);
                // for calling in Update
                _outAction = item;
            }

            // print word spoken by user
            print(message);
        }
        
        private void Update()
        {
            // call functions for menu controls
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


        // menu buttons/commands
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