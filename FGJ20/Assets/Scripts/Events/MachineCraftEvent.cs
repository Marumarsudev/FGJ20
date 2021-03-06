﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MachineCraftEvent : BaseEvent
{
    public CraftingRecipe recipe;

    public InventoryItem repairItem;

    public bool isRepaired;

    public float craftTime;
    private float craftTimer;

    private bool crafted = false;

    public GameObject craftedWorldItem;

    public Transform spawnPoint;

    public TextMeshPro timnertext;

    private Color origTextColor;

    void Start()
    {
        origTextColor = timnertext.color;
    }

    public override void CallEvent(GameObject interactor = null)
    {
        if(interactor && isRepaired)
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
        if(isRepaired)
        {
            timnertext.color = origTextColor;
        }
        else
        {
            timnertext.color = Color.red;
        }

        if(crafted)
        {
            craftTimer -= Time.deltaTime;
            timnertext.text = craftTimer.ToString("n2");
        }
        else if (isRepaired)
        {
            timnertext.text = "00.00";
        }
        else
        {
            timnertext.text = "BROKEN";
        }

        if(craftTimer <= 0 && crafted)
        {
            crafted = false;
            Instantiate(craftedWorldItem, spawnPoint.position, Quaternion.identity);
        }
    }

}
