using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemEvent : BaseEvent
{
    public CraftingRecipe recipe;

    public override void CallEvent(GameObject interactor = null)
    {
        if(interactor)
        {
            if(interactor.GetComponent<PlayerInventory>())
            {
                PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();

                inventory.AddItem(recipe.Craft(inventory), recipe.amount);
            }
        }
    }
}
