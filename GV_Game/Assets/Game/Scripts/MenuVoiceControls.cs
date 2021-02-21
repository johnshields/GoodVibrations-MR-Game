using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Game.Scripts
{
    public class MenuVoiceControls : MonoBehaviour
    {
        private GrammarRecognizer _grammarRecognizer;

        private void Start()
        {
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, 
                "MenuControls.xml"), ConfidenceLevel.Low);
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            Debug.Log("Menu Voice Controls loaded...");
        }

        private static void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
        {
            var message = new StringBuilder();
            // read the semantic meanings from the args passed in.
            var meanings = args.semanticMeanings;

            foreach (var  meaning in meanings)
            {
                var keyString = meaning.key.Trim();
                var valueString = meaning.values[0].Trim();
                message.Append("Key" + keyString + ", Value" + valueString);
            }
            Debug.Log(message);
        }

        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}