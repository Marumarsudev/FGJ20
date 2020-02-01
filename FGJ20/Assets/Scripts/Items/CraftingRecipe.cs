using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RecipeItem")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField]
    public List<RecipeItem> requiredItems = new List<RecipeItem>();

    public InventoryItem result;

    public int amount;

    public InventoryItem Craft(PlayerInventory inventory)
    {
        if(CheckCraftability(inventory))
        {
            requiredItems.ForEach(item => {
                inventory.RemoveItem(item.item, item.amount);
            });

            return result;
        }
        else
        {
            return null;
        }
    }

    public bool CheckCraftability(PlayerInventory inventory)
    {
        int hasAll = 0;

        foreach (RecipeItem recipeItem in requiredItems)
        {
            if(inventory.CheckItem(recipeItem.item, recipeItem.amount))
            {
                hasAll++;
            }
        }

        if(hasAll == requiredItems.Count)
        {
            return true;
        }
        else
        {
            Debug.Log("Cannot craft item, not enough items!");
            return false;
        }
    }
}

[System.Serializable]
public class RecipeItem
{
    public InventoryItem item;
    public int amount;
}
