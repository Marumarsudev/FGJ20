using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairMachineEvent : BaseEvent
{
    public MachineCraftEvent machineCraftEvent;

    public string InfoText
    {
        get
        {
            if(!machineCraftEvent.isRepaired)
                return "Press 'E' to use " + machineCraftEvent.repairItem.itemName + " to repair the machine.";
            else
                return "";
        }
    }

    public override void CallEvent(GameObject interactor = null)
    {
        if(interactor && !machineCraftEvent.isRepaired)
        {
            if(interactor.GetComponent<PlayerInventory>())
            {
                PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();

                if(inventory.CheckItem(machineCraftEvent.repairItem, 1))
                {
                    inventory.RemoveItem(machineCraftEvent.repairItem, 1);
                    machineCraftEvent.isRepaired = true;
                }
            }
        }
    }
}
