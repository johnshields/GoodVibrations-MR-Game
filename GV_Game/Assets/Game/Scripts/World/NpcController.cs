using System.Collections;
using UnityEngine;

namespace Game.Scripts.World
{
    public class NpcController : MonoBehaviour
    {
        // NPC dogs stats
        [SerializeField] public float profile = 2f;
        [SerializeField] public float rotate = 100f;
        private bool _rotateLeftActive;
        private bool _rotateRightActive;
        private bool _walkActive;
        
        private void Update()
        {
            // call function to move NPC dogs
            RandomMovement();
        }

        // make the NPC dogs move in different positions
        private void RandomMovement()
        {
            if (_walkActive == false)
            {
                StartCoroutine(Wander());
            }
            if (_walkActive)
            {
                transform.position += transform.forward * (profile * Time.deltaTime);
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
            var rotationTime = Random.Range(1, 3);
            var rotateWait = Random.Range(3, 4);
            var rotateLorR = Random.Range(1, 2);
            var walkWait = Random.Range(1, 4);
            var walkTime = Random.Range(1, 5);

            // NPC dogs walk and rotate
            yield return new WaitForSeconds(walkWait);
            _walkActive = true;
            yield return new WaitForSeconds(walkTime);
            _walkActive = false;
            yield return new WaitForSeconds(rotateWait);
            
            // switch between right and left rotation
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