using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTravel : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public float damage;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        Invoke("TimedDestroy", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.GetComponent<HealthComponent>())
        {
            col.collider.GetComponent<HealthComponent>().TakeHealth(damage);
        }

        Destroy(this.gameObject);
    }

    void TimedDestroy()
    {
        Destroy(this.gameObject);
    }
}
