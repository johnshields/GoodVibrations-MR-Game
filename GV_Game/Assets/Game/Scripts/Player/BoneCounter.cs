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
        public int bones;
        private GameObject _boneCounter;

        private void Start()
        {
            // find BoneCounter & load Player Bones
            _boneCounter = GameObject.Find("CounterCanvas/Text");
            bones = PlayerPrefs.GetInt("bones");
        }

        private void Awake()
        {
            // reset Bones back to 0
            PlayerPrefs.SetInt("bones", 0);
        }

        private void Update()
        {
            // save Bones to Player
            PlayerPrefs.SetInt("bones", bones);
        }

        // display Bone Counter
        private void OnGUI()
        {
            // find Bones Counter & add the updated Bones amount
            var bonesUI = _boneCounter.GetComponent<Text>();
            bonesUI.text = "BONES: " + bones;
        }
    }
}