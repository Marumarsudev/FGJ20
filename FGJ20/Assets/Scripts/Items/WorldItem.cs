using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{

    public List<BaseEvent> InteractEvent = new List<BaseEvent>();

    public bool interactWithE;

    public void Interact(GameObject interactor = null)
    {
        InteractEvent.ForEach(e => {
            e.CallEvent(interactor);
        });
    }
}
