using Game.Scripts.Player;
using UnityEngine;

/*
 * John Shields - G00348436
 * Bone
 *
 * For Rotating Bones, only allowing the Player to collect to Bones.
 * Plus adds collected Bones to the BoneCounter.
*/
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
            // find BoneCounter & Player
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
            // only detect collisions from Player
            if (other.gameObject != _dog) return;
            var position = transform.position;
            // pickup sounds
            AudioSource.PlayClipAtPoint(pickupSound, position);
            AudioSource.PlayClipAtPoint(bark, position);
            // destroy Bone & add to the Bone Counter
            Destroy(gameObject);
            _boneCounter.GetComponent<BoneCounter>().bones += 1;
        }
    }
}