using System.Collections;
using UnityEngine;

namespace Caeca.Control
{   
    /// <summary>
    /// Slowly enables all children of this transform
    /// </summary>
    public class SequentialChildEnabler : MonoBehaviour
    {
        [SerializeField] private float minDelay = 0.5f;
        [SerializeField] private float maxDelay = 0.5f;

        private void Start() {
            StartCoroutine(EnableChildren());
        }

        private IEnumerator EnableChildren()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            }
        }
    }
}
