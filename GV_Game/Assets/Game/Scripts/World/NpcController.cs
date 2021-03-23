using System.Collections;
using UnityEngine;

/*
 * John Shields - G00348436
 * NpcController
 *
 * Makes the NPC Dogs walk in different positions and rotate in random ranges.
 * Reference - https://youtu.be/aEPSuGlcTUQ
*/
namespace NpcDogs
{
    public class NpcController : MonoBehaviour
    {
        // NPC dogs stats for movement.
        [SerializeField] public float profile = 1f;
        [SerializeField] public float rotate = 100f;
        private bool _rotateLeftActive;
        private bool _rotateRightActive;
        private bool _walkActive;

        // If the NPCs are not walking start a Coroutine to move them in different positions.
        // Plus change between walking and rotating.
        private void Update()
        {
            if (_walkActive == false)
            {
                StartCoroutine(Wander());
            }
         
            if (_walkActive)
            {
                var trans = transform;
                trans.position += trans.forward * (profile * Time.deltaTime);
            }

            if (_rotateRightActive)
            {
                transform.Rotate(transform.up * (Time.deltaTime * rotate));
            }

            if (_rotateLeftActive)
            {
                transform.Rotate(transform.right * (Time.deltaTime * -rotate));
            }
        }

        private IEnumerator Wander()
        {
            // Set random Range for Rotations and Walking.
            var rotationTime = Random.Range(1, 3);
            var rotateWait = Random.Range(3, 4);
            var rotateLorR = Random.Range(1, 2);
            var walkWait = Random.Range(1, 4);
            var walkTime = Random.Range(1, 6);

            // Use Random Ranges to make walk and rotate NPCs.
            yield return new WaitForSeconds(walkWait);
            _walkActive = true;
            yield return new WaitForSeconds(walkTime);
            _walkActive = false;
            yield return new WaitForSeconds(rotateWait);

            // Switch between right and left rotation.
            switch (rotateLorR)
            {
                case 1:
                    _rotateRightActive = true;
                    yield return new WaitForSeconds(rotationTime);
                    _rotateRightActive = false;
                    break;
                case 2:
                    _rotateLeftActive = true;
                    yield return new WaitForSeconds(rotationTime);
                    _rotateLeftActive = false;
                    break;
            }
        }
    }
}