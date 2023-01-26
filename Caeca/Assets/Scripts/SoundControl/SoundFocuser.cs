using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    public class SoundFocuser : MonoBehaviour
    {
        [SerializeField] private SoundReceiver soundReceiver;

        [SerializeField] private int focusedIndex = -1;
        private ISoundEmitting focusedEmmiter;

        private void Start()
        {
            StartCoroutine(SlowUpdate());
        }

        private IEnumerator SlowUpdate()
        {
            yield return new WaitForSeconds(0.1f);

            if (focusedIndex >= 0)
                Focus();
            else
            {
                foreach (ISoundEmitting emitter in soundReceiver.focusableEmitters)
                    emitter.Focus();
                soundReceiver.TriggerFocusEvent(false);
            }

            soundReceiver.TriggerTickEvent(0.1f);

            StartCoroutine(SlowUpdate());
        }

        private void Focus()
        {
            soundReceiver.TriggerFocusEvent(true);
            foreach (ISoundEmitting emitter in soundReceiver.focusableEmitters)
                emitter.Unfocus();
            soundReceiver.focusableEmitters[focusedIndex].Focus();
        }

    }
}
