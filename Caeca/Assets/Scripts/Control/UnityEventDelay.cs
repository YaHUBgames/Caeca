using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Caeca.Control
{
    /// <summary>
    /// Function that triggers unityEvent after a delay.
    /// </summary>
    public class UnityEventDelay : MonoBehaviour
    {
        public UnityEvent OnDelayedEvent;

        public void DelayEvent(float _delay)
        {
            StartCoroutine(Delay(_delay));
        }

        private IEnumerator Delay(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            OnDelayedEvent?.Invoke();
        }
    }
}
