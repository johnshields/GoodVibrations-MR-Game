using System.Collections;
using Game.Scripts.Player;
using Game.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menus
{
    public class GameWonMenu : MonoBehaviour
    {
        private Component _boneCounter;
        public GameObject gameWon;
        private Component _musicPlayer;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            _boneCounter = GameObject.Find("Player").GetComponent<BoneCounter>();
            _musicPlayer = GameObject.Find("Music Player").GetComponent<MusicPlayer>();
            _audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
            gameWon.SetActive(false);
        }

        private void Update()
        {
            GameWon();
        }
        
        // Game ends when all bones are collected
        private void GameWon()
        {
            if (_boneCounter.GetComponent<BoneCounter>().bones == 20)
            {
                // display game won menu
                gameWon.SetActive(true);
                Cursor.visible = true;
                _audioSource.GetComponent<AudioSource>().enabled = false;
                _musicPlayer.GetComponent<MusicPlayer>().enabled = false;
                // to the main menu
                StartCoroutine(FadeOutMainMenu());
                
            }
        }


        // fade the scene out to main menu
        private static IEnumerator FadeOutMainMenu()
        {
            yield return new WaitForSeconds(4);
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
