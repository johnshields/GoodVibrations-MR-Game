using UnityEngine.UI;
using UnityEngine;

/*
 * John Shields - G00348436
 * BoneCounter
 * 
 * For Saving Bones to Player Prefs & displaying the collected Bones in game.
*/
namespace Game.Scripts.Player
{
    public class BoneCounter : MonoBehaviour
    {
        // Int to collect bones and GameObject for BoneCounter GUI.
        public int bones;
        private GameObject _boneCounter;

        // Find BoneCounter and load Player Bones.
        private void Start()
        {
            _boneCounter = GameObject.Find("CounterCanvas/Text");
            bones = PlayerPrefs.GetInt("bones");
        }

        // Set/reset Bones to 0 on Awake.
        private void Awake()
        {
            PlayerPrefs.SetInt("bones", 0);
        }

        // Save Bones to Player.
        private void Update()
        {
            PlayerPrefs.SetInt("bones", bones);
        }

        // Add the updated Bones amount to BoneCounter.
        private void OnGUI()
        {
            var counterUI = _boneCounter.GetComponent<Text>();
            counterUI.text = "BONES: " + bones;
        }
    }
}