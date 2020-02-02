using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomItemEvent : BaseEvent
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public override void CallEvent(GameObject interactor = null)
    {
        if(interactor != null)
        {
            if(interactor.GetComponent<PlayerInventory>())
            {

                interactor.GetComponent<PlayerInventory>().AddItem(items[Random.Range(0, items.Count)], 1);

            }
        }
    }

}
