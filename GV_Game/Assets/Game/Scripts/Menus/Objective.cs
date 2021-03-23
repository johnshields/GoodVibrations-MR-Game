using UnityEngine;
using System.Collections;

/*
 * John Shields - G00348436
 * Objective
 * 
 * Display Objective when Scene Awakes and fade it away after 5 seconds.
*/
namespace Game.Scripts.Menus
{
    public class Objective : MonoBehaviour
    {
        [SerializeField] public GameObject objective;

        // Show objective on Awake and start a Coroutine to disable it.
        private void Awake()
        {
            objective.SetActive(true);
            StartCoroutine(DisableObj());
        }

        // Disable objective after 5 seconds.
        private IEnumerator DisableObj()
        {
            yield return new WaitForSeconds(5);
            objective.SetActive(false);
        }
    }
}