using UnityEngine;
using Random = UnityEngine.Random;

/*
 * John Shields - G00348436
 * MusicPlayer
 *
 * Uses an Audio Source to shuffle and play music from a playlist array of 11 songs.
*/
namespace Game.Scripts.World
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioClip[] playlist;
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
            // pick a random song to play from the audio source
            if (_audioSource.isPlaying) return;
            _audioSource.clip = playlist[Random.Range(0, playlist.Length)];
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}