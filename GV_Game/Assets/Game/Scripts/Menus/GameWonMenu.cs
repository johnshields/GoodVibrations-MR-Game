using System.Collections;
using Game.Scripts.Player;
using Game.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menus
{
    public class GameWonMenu : MonoBehaviour
    {
        [SerializeField] public GameObject gameWon;
        private Component _boneCounter;
        private Component _musicPlayer;
        private AudioSource _audioSource;
        private AudioSource _wonAudioSource;

        private void Awake()
        {
            _boneCounter = GameObject.Find("Player").GetComponent<BoneCounter>();
            _musicPlayer = GameObject.Find("Music Player").GetComponent<MusicPlayer>();
            _audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
            _wonAudioSource = GameObject.Find("Game Won").GetComponent<AudioSource>();
            gameWon.SetActive(false);
        }

        private void Update()
        {
            GameWon();
        }
        
        // Game ends when all bones are collected
        private void GameWon()
        {
            if (_boneCounter.GetComponent<BoneCounter>().bones == 20 && !_wonAudioSource.isPlaying)
            {
                // display game won menu
                gameWon.SetActive(true);
                Cursor.visible = true;
                // disable music
                _audioSource.GetComponent<AudioSource>().enabled = false;
                _musicPlayer.GetComponent<MusicPlayer>().enabled = false;
                // play game won sound
                _wonAudioSource.Play();
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
