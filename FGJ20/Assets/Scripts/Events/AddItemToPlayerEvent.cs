using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemToPlayerEvent : BaseEvent
{
    InventoryItem item;

    public int amountMin, amountMax;

    private HealthComponent hp;

    void Start()
    {
        item = GetComponent<WorldItem>().inventoryItem;

        if(GetComponent<HealthComponent>())
        {
            hp = GetComponent<HealthComponent>();
        }
    }

    public override void CallEvent(GameObject interactor = null)
    {
        if(interactor != null)
        {
            if(interactor.GetComponent<PlayerInventory>())
            {
                int amount = Random.Range(amountMin, amountMax);

                if(hp != null)
                {
                    if(hp.CurrentHealth < amount)
                        amount = (int)hp.CurrentHealth;
                }

                interactor.GetComponent<PlayerInventory>().AddItem(item, amount);

                if(hp != null)
                {
                    hp.TakeHealth(amount);
                }
            }
        }
    }
}
