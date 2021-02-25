using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.World
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioClip[] tunes;
        private Component _musicPlayer;
        private AudioSource _audioSource;

        private void Awake()
        {
            // find music player
            _musicPlayer = GameObject.Find("Music Player").GetComponent<MusicPlayer>();
            _audioSource = GameObject.Find("Music Player").GetComponent<AudioSource>();
            _musicPlayer.GetComponent<MusicPlayer>().enabled = true;
            _audioSource.GetComponent<AudioSource>().enabled = true;
            
        }

        private void Update()
        {
            // if the audio source is not playing pick a random tune to play
            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = tunes[Random.Range(0, tunes.Length)];
                _audioSource.PlayOneShot(_audioSource.clip);
            }
        }
    }
}