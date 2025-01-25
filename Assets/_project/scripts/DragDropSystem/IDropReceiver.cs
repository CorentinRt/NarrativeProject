using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public interface IDropReceiver
    {
        abstract bool CanReceive(GameObject draggable);

        abstract void OnReceiveDragBeginOverlapping(GameObject draggable);
        abstract void OnReceiveDragEndOverlapping(GameObject draggable);

        abstract void OnReceiveDropDraggable(GameObject draggable);
    }
}
