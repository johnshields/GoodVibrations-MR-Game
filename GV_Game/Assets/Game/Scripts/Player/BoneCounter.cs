using UnityEngine.UI;
using UnityEngine;

namespace Game.Scripts
{
    public class BoneCounter : MonoBehaviour
    {
        public int bones;

        private void Start()
        {
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
            var boneTxt = GameObject.Find("CounterCanvas/Text").GetComponent<Text>();
            // and add the updated Bones amount
            boneTxt.text = "BONES: " + bones;
        }
    }
}