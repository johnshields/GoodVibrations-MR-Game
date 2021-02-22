using UnityEngine;

namespace Game.Scripts.Menus
{
    public class FadeMusic : MonoBehaviour
    {
        private static Animator _animator;
        private static int _fadeOut;

        private void Start()
        {
            _fadeOut = Animator.StringToHash("FadeOut");
            _animator = GetComponent<Animator>();
        }
        
        public static void FadeOutMusic()
        {
            // set animation trigger to fade music out
            _animator.SetTrigger(_fadeOut);
        }
    }
}
