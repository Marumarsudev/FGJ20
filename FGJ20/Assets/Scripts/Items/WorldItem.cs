using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{

    public InventoryItem inventoryItem;

    public List<BaseEvent> InteractEvent = new List<BaseEvent>();

    public void Interact(GameObject interactor = null)
    {
        InteractEvent.ForEach(e => {
            e.CallEvent(interactor);
        });
    }
}
