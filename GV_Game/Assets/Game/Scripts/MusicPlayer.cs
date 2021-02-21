using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioClip[] tunes;
        private AudioSource _audioSource;
        
        private void Awake()
        {
            // find music player
            _audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
        }
        
        private void Update()
        {
            // if the audio source is not playing pick a random tune to play
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip=tunes[Random.Range(0, tunes.Length)];
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }

    }
}
