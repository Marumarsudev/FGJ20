using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public float CurrentHealth { get {return currentHealth;} }

    public bool destroyOnDeath;

    public List<BaseEvent> deathEvents = new List<BaseEvent>();

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void GiveHealth(float amount)
    {
        amount = Mathf.Abs(amount);

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void TakeHealth(float amount, GameObject interactor = null)
    {
        amount = Mathf.Abs(amount);

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        if(currentHealth <= 0)
        {
            Death(interactor);
        }
    }

    private void Death(GameObject interactor = null)
    {
        deathEvents.ForEach(e => {
            e.CallEvent(interactor);
        });

        if(destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

}
