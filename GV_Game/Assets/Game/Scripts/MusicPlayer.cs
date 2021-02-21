using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioClip[] tunes;
        private AudioSource _audioSource;
        
        private void Start()
        {
            _audioSource.loop = false;
            _audioSource.clip=tunes[Random.Range(0, tunes.Length)];
            _audioSource.PlayOneShot(_audioSource.clip);
        }

        private void Awake()
        {
            _audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
        }

    }
}
