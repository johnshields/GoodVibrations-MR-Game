using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.World
{
    public class Bone : MonoBehaviour
    {
        [SerializeField] public AudioClip pickupSound;
        [SerializeField] public AudioClip bark;

        private Component _boneCounter;
        private GameObject _dog;


        private void Start()
        {
            _boneCounter = GameObject.Find("Player").GetComponent<BoneCounter>();
            _dog = GameObject.Find("Player");
        }

        private void Update()
        {
            // rotate Bone
            transform.Rotate(new Vector3(1f, 1f, 1f));
        }

        private void OnTriggerEnter(Collider other)
        {
            // only detect collisions from player
            if (other.gameObject == _dog)
            {
                var position = transform.position;
                // pickup sounds
                AudioSource.PlayClipAtPoint(pickupSound, position);
                AudioSource.PlayClipAtPoint(bark, position);
                // destroy Bone
                Destroy(gameObject);
                // add to the Bone Counter
                _boneCounter.GetComponent<BoneCounter>().bones += 1;
            }
        }
    }
}