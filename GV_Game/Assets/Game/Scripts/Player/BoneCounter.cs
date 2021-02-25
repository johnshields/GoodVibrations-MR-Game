using UnityEngine.UI;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class BoneCounter : MonoBehaviour
    {
        public int bones;
        private GameObject _boneCounter;

        private void Start()
        {
            _boneCounter = GameObject.Find("CounterCanvas/Text");
            // load Player Bones
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
            // find Bones Counter
            var boneTxt = _boneCounter.GetComponent<Text>();
            // and add the updated Bones amount
            boneTxt.text = "BONES: " + bones;
        }

 
    }
}