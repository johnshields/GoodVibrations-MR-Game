using UnityEngine;

/*
 * John Shields - G00348436
 * FadeMusic
 * 
 * For calling in other Scripts to fade the music out using an animation. 
*/
namespace Game.Scripts.Menus
{
    public class FadeMusic : MonoBehaviour
    {
        private static Animator _animator;
        private static int _fadeOut;

        private void Start()
        {
            // hash int to get animator trigger
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