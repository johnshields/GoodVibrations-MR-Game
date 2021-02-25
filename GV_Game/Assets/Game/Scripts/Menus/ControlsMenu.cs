using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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