using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * John Shields - G00348436
 * SceneChanger
 * 
 * For calling in other Scripts to have a black screen fade.
 * out animation when the scene changes.
 * Reference - https://youtu.be/Oadq-IrOazg
*/
namespace Game.Scripts.Menus
{
    public class SceneChanger : MonoBehaviour
    {
        // Animator and ints for animator boolean & SceneManager.
        private static Animator _animator;
        private static int _fadeOut;
        private static int _nextSceneToLoad;

        // Hash int to get animator trigger & get next scene.
        private void Start()
        {
            _fadeOut = Animator.StringToHash("FadeOut");
            _animator = GetComponent<Animator>();
            _nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        // Set animation trigger to fade scene out.
        public static void FadeToScene()
        {
            _animator.SetTrigger(_fadeOut);
        }

        // Load the next scene.
        public static void NextScene()
        {
            SceneManager.LoadScene(_nextSceneToLoad);
        }
    }
}