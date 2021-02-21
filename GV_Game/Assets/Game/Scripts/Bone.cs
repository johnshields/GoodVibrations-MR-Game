using UnityEngine;

namespace Game.Scripts
{
    public class Bone : MonoBehaviour
    {
        [SerializeField] public AudioClip pickupSound;
        [SerializeField] public AudioClip bark;

        private void Update()
        {
            // rotate Bone
            transform.Rotate(new Vector3(1f, 1f, 1f));
        }

        private void OnTriggerEnter(Collider other)
        {
            var position = transform.position;
            // pickup sounds
            AudioSource.PlayClipAtPoint(pickupSound, position);
            AudioSource.PlayClipAtPoint(bark, position);
            // destroy Bone
            Destroy(gameObject);
            // add to the Bone Counter
            other.GetComponent<BoneCounter>().bones += 1;
            Debug.Log("Bone Collected!");
        }
    }
}
