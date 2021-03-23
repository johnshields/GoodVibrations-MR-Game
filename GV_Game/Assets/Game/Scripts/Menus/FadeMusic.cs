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
        // Animator & int for animation bool.
        private static Animator _animator;
        private static int _fadeOut;

        // Hash int to get animator trigger.
        private void Start()
        {
            _fadeOut = Animator.StringToHash("FadeOut");
            _animator = GetComponent<Animator>();
        }

        // Set animation trigger to fade music out.
        public static void FadeOutMusic()
        {
            _animator.SetTrigger(_fadeOut);
        }
    }
}