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
        public void BackMainMenu()
        {
            // to the main menu
            StartCoroutine(FadeOutMainMenu());
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