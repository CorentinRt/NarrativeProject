using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class MenuStartTransitionDelay : MonoBehaviour
    {
        [SerializeField] private float _startTransitionDelay;

        public UnityEvent OnStartTransitionDelayOverUnity;

        private void Start()
        {
            StartCoroutine(StartTransitionDelayCoroutine());
        }

        private IEnumerator StartTransitionDelayCoroutine()
        {
            yield return new WaitForSeconds(_startTransitionDelay);

            OnStartTransitionDelayOverUnity?.Invoke();

            yield return null;
        }
    }
}
