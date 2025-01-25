using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NarrativeProject
{
    public class DropZoneTest : MonoBehaviour, IDropReceiver
    {
        #region Fields
        private bool _bIsOvered;


        #endregion


        public UnityEvent OnReceiveDropUnity;
        public UnityEvent OnBeginDragOverlappingUnity;
        public UnityEvent OnEndDragOverlappingUnity;


        #region DropReceiver Interface
        public bool CanReceive(GameObject draggable)
        {
            return true;
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
