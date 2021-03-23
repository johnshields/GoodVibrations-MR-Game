using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * John Shields - G00348436
 * ControlsMenu
 * 
 * For UI Button to go back to Main Menu.
*/
namespace Game.Scripts.Menus
{
    public class ControlsMenu : MonoBehaviour
    {
        // Start a Coroutine to go back the main menu.
        public void BackMainMenu()
        {
            StartCoroutine(FadeOutMainMenu());
        }

        // Fade the music & scene out to main menu.
        private static IEnumerator FadeOutMainMenu()
        {
            FadeMusic.FadeOutMusic();
            SceneChanger.FadeToScene();
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}