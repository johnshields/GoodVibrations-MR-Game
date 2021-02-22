using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menus
{
    public class SceneChanger : MonoBehaviour
    {
        private static Animator _animator;
        private static int _fadeOut;
        private static int _nextSceneToLoad;

        private void Start()
        {
            _fadeOut = Animator.StringToHash("FadeOut");
            _animator = GetComponent<Animator>();
            _nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        public static void FadeToScene()
        {
            // set animation trigger to fade scene out
            _animator.SetTrigger(_fadeOut);
        }

        public static void NextScene ()
        {
            // load the next scene
            SceneManager.LoadScene(_nextSceneToLoad);
        }
        
        
    }
}