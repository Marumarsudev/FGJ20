using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int inventorySize;

    private Dictionary<InventoryItem, int> inventory = new Dictionary<InventoryItem, int>();

    public TextMeshProUGUI inventoryText;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            DebugPrintItems();
        }
    }

    private void UpdateInventoryUI()
    {
        string itemlist = "Inventory:";

        foreach (KeyValuePair<InventoryItem, int> keyValuePair in inventory)
        {
            itemlist += "\n" + keyValuePair.Key.name + " : " + keyValuePair.Value.ToString();
        }

        inventoryText.text = itemlist;
    }

    public void DebugPrintItems()
    {
        foreach (KeyValuePair<InventoryItem, int> keyValuePair in inventory)
        {
            Debug.Log(keyValuePair.Key.name + " : " + keyValuePair.Value.ToString());
        }
    }

    public bool CheckItem(InventoryItem item, int amount)
    {
        if(inventory.ContainsKey(item))
        {
            if(inventory[item] >= amount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void AddItem(InventoryItem item, int amount)
    {
        if(item == null)
        {
            Debug.Log("No item provided !"); 
            return;
        }

        if(inventory.ContainsKey(item))
        {
            inventory[item] += amount;
            Debug.Log("Added " + amount.ToString() + " of " + item.itemName);
        }
        else if(inventory.Count < inventorySize)
        {
            inventory.Add(item, amount);
            Debug.Log("Added " + amount.ToString() + " of " + item.itemName);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }

        UpdateInventoryUI();
    }

    public void RemoveItem(InventoryItem item, int amount)
    {
        if(inventory.ContainsKey(item))
        {
            if(inventory[item] >= amount)
            {
                inventory[item] -= amount;
                if(inventory[item] == 0)
                {
                    inventory.Remove(item);
                }
                Debug.Log("Removed " + amount.ToString() + " of " + item.itemName);
            }
        }
        else
        {
            Debug.Log("No such item in inventory");
        }
        UpdateInventoryUI();
    }
}
