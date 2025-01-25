using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NarrativeProject
{
    public class DropZone : MonoBehaviour, IDropReceiver
    {
        #region Fields
        private bool _bIsOvered;

        private bool _canReceive = true;

        #endregion


        public UnityEvent OnReceiveDropUnity;
        public UnityEvent OnBeginDragOverlappingUnity;
        public UnityEvent OnEndDragOverlappingUnity;


        #region DropReceiver Interface
        public bool CanReceive(GameObject draggable)
        {
            return _canReceive;
        }
        public void OnReceiveDropDraggable(GameObject draggable)
        {
            OnReceiveDropUnity?.Invoke();
        }

        public void OnReceiveDragBeginOverlapping(GameObject draggable)
        {
            _bIsOvered = true;
            OnBeginDragOverlappingUnity?.Invoke();
        }

        public void OnReceiveDragEndOverlapping(GameObject draggable)
        {
            _bIsOvered = false;
            OnEndDragOverlappingUnity?.Invoke();
        }

        #endregion


    }
}
