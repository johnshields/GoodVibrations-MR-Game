using System.Collections;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * John Shields - G00348436
 * PauseMenu
 * 
 * Pause the game if pause button is pressed plus buttons to resume game and go back to Main Menu.
*/
namespace Game.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        // Pause Menu & Player
        public GameObject pauseMenu;
        private bool _paused;
        private GameObject _dog;
        
        private void Start()
        {
            // find Player, turn off cursor & set pause menu to false
            _dog = GameObject.Find("Player");
            Cursor.visible = false;
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
            Cursor.visible = true; // turn on cursor
            Time.timeScale = 0f; // stop time
            _paused = true; // game is paused
        }

        public void ResumeGame()
        {
            // for enabling mouse player movement
            _dog.GetComponent<DogController>().enabled = true;
            // resume game
            pauseMenu.SetActive(false);
            Cursor.visible = false;  // turn off cursor
            Time.timeScale = 1f; // resume time
            _paused = false; // game is unpaused
        }

        public void BackMainMenu()
        {
            // to the main menu
            StartCoroutine(FadeOutMainMenu());
            Time.timeScale = 1f;
        }

        // fade the music & scene out to main menu
        private static IEnumerator FadeOutMainMenu()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}