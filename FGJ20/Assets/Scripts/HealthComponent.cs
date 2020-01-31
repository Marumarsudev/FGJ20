using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public bool destroyOnDeath;

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

    public void TakeHealth(float amount)
    {
        amount = Mathf.Abs(amount);

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        if(destroyOnDeath)
        {
            Destroy(gameObject);
        }
    }

}
