using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineCraftEvent : BaseEvent
{
    public CraftingRecipe recipe;

    public float craftTime;
    private float craftTimer;

    private bool crafted = false;

    public GameObject craftedWorldItem;

    public Transform spawnPoint;

    public override void CallEvent(GameObject interactor = null)
    {
        if(interactor)
        {
            if(interactor.GetComponent<PlayerInventory>())
            {
                PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();

                if(recipe.Craft(inventory) != null)
                {
                    Debug.Log("Machine started crafting!");
                    crafted = true;
                    craftTimer = craftTime;
                }
            }
        }
    }

    void Update()
    {
        if(crafted)
        {
            craftTimer -= Time.deltaTime;
            Debug.Log(craftTimer);
        }

        if(craftTimer <= 0 && crafted)
        {
            crafted = false;
            Instantiate(craftedWorldItem, spawnPoint.position, Quaternion.identity);
        }
    }

}
