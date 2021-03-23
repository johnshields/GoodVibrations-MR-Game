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
        // Pickup sounds, Bone Counter and Player.
        [SerializeField] public AudioClip pickupSound;
        [SerializeField] public AudioClip bark;
        private Component _boneCounter;
        private GameObject _dog;

        // Find BoneCounter and Player.
        private void Start()
        {
            _boneCounter = GameObject.Find("Player").GetComponent<BoneCounter>();
            _dog = GameObject.Find("Player");
        }

        // Rotate Bone every frame.
        private void Update()
        {
            transform.Rotate(new Vector3(1f, 1f, 1f));
        }

        // Allows only the Player to collect the bones. Plus adds collected bones to the Bone Counter.
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != _dog) return;
            var position = transform.position;
            AudioSource.PlayClipAtPoint(pickupSound, position);
            AudioSource.PlayClipAtPoint(bark, position);
            // Destroy Bone and add to the Bone Counter.
            Destroy(gameObject);
            _boneCounter.GetComponent<BoneCounter>().bones += 1;
        }
    }
}