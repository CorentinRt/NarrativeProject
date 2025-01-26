using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarrativeProject
{
    public interface IInputsInteractible
    {
        public event Action<IInputsInteractible, EPlayerInputsState> OnInteractionBegin;
        public event Action<IInputsInteractible, EPlayerInputsState> OnInteractionEnd;
        public abstract void SetIsAbleToInteract(bool value);

        public abstract bool CanInteract();

        public abstract void NotifyInteraction(EPlayerInputsState inputState);
        public abstract void NotifyEndInteraction();
    }
}
