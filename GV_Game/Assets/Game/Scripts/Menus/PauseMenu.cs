using System.Collections;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        // pause menu
        public GameObject pauseMenu;
        private bool _paused;
        
        private GameObject _dog;
        
        private void Start()
        {
            _dog = GameObject.Find("Player");

            // set pause menu to false
            pauseMenu.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (_paused)
            {
                ResumeGame();
                AudioListener.volume = 1f;
            }
            else
            {
                PauseGame();
                AudioListener.volume = 0f;
            }
        }

        private void PauseGame()
        {
            // for disabling mouse player movement
            _dog.GetComponent<DogController>().enabled = false;
            // pause game
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.volume = 0f; // pause audio
            _paused = true; // game is paused
        }

        public void ResumeGame()
        {
            _dog.GetComponent<DogController>().enabled = true;
            // resume game
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _paused = false;
            AudioListener.volume = 1f;
        }

        public void BackMainMenu()
        {
            // to the main menu
            StartCoroutine(FadeOutMainMenu());
            Time.timeScale = 1f;
        }

        private static IEnumerator FadeOutMainMenu()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}