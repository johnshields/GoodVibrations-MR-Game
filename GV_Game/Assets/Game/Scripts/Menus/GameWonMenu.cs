using System.Collections;
using Game.Scripts.Player;
using Game.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * John Shields - G00348436
 * GameWonMenu
 * 
 * Displays a Banner when the Player has collected all 20 Bones and Fades to the Main Menu.
*/
namespace Game.Scripts.Menus
{
    public class GameWonMenu : MonoBehaviour
    {
        [SerializeField] public GameObject gameWon;
        private Component _boneCounter;
        private BoneCounter _boneScore;
        private Component _musicPlayer;
        private AudioSource _audioSource;
        private AudioSource _wonAudioSource;

        private void Awake()
        {
            // find necessary components & set Game Won Menu to false
            _boneCounter = GameObject.Find("Player").GetComponent<BoneCounter>();
            _boneScore = _boneCounter.GetComponent<BoneCounter>();
            _musicPlayer = GameObject.Find("Music Player").GetComponent<MusicPlayer>();
            _audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
            _wonAudioSource = GameObject.Find("Game Won").GetComponent<AudioSource>();
            gameWon.SetActive(false);
        }

        private void Update()
        {
            // Game ends when all bones are collected
            if (_boneScore.bones != 20 || _wonAudioSource.isPlaying) return;
            // display Game Won Menu & disable music
            gameWon.SetActive(true);
            _audioSource.GetComponent<AudioSource>().enabled = false;
            _musicPlayer.GetComponent<MusicPlayer>().enabled = false;
            // play game won sound & go to the main menu
            _wonAudioSource.Play();
            StartCoroutine(FadeOutMainMenu());
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
