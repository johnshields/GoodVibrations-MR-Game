using UnityEngine;
using System.Collections;

namespace Game.Scripts.Menus
{
    public class Objective : MonoBehaviour
    {
        [SerializeField] public GameObject objective;

        private void Awake()
        {
            // show objective
            objective.SetActive(true);
            StartCoroutine(DisableObj());
        }
        
        private IEnumerator DisableObj()
        {
            // disable objective
            yield return new WaitForSeconds(5);
            objective.SetActive(false);
        }
    }
}
