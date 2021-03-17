using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * John Shields - G00348436
 * SceneChanger
 * 
 * For calling in other Scripts to have a black screen fade
 * out animation when the scene changes.
*/
namespace Game.Scripts.Menus
{
    public class SceneChanger : MonoBehaviour
    {
        private static Animator _animator;
        private static int _fadeOut;
        private static int _nextSceneToLoad;

        private void Start()
        {
            // hash int to get animator trigger & get next scene 
            _fadeOut = Animator.StringToHash("FadeOut");
            _animator = GetComponent<Animator>();
            _nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        public static void FadeToScene()
        {
            // set animation trigger to fade scene out
            _animator.SetTrigger(_fadeOut);
        }

        public static void NextScene()
        {
            // load the next scene
            SceneManager.LoadScene(_nextSceneToLoad);
        }
    }
}