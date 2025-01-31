using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NarrativeProject
{
    public class DialogDisplayerDebug : MonoBehaviour
    {
        public UnityEvent OnClickShowUnity;
        public UnityEvent OnClickHideUnity;


        [Button]
        private void ClickShow()
        {
            OnClickShowUnity?.Invoke();
        }
        [Button]
        private void ClickHide()
        {
            OnClickHideUnity?.Invoke();
        }
    }
}
