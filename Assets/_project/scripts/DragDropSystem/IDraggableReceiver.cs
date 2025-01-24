using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggableReceiver
{
    public abstract void OnReceiveDragObject(DragDropObject dragDropObject);
}
