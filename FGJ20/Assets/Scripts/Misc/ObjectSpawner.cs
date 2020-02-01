using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> environment = new List<GameObject>();

    public float spawnRadius;

    public Terrain terrain;

    public Transform parent;

    public int enemycount;
    public int environmentcount;

    private int layerMask;

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~(1 << 9);
        Debug.Log(layerMask);
        for(int i = 0; i < enemycount; i++)
        {
            Vector3 spawnpos;
            spawnpos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + transform.position;

            spawnpos.y = terrain.SampleHeight(spawnpos);

            if(Physics.OverlapSphere(spawnpos, 3f, mask).Length  == 0)
            {
                GameObject temp;
                temp = Instantiate(enemies[Random.Range(0, enemies.Count)], spawnpos, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), parent);

                enemies.Add(temp);
            }
            else
            {
                //i--;
                Debug.Log("Oh no we hit something");
            }
        }

        for(int i = 0; i < environmentcount; i++)
        {
            Vector3 spawnpos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + transform.position;

            spawnpos.y = terrain.SampleHeight(spawnpos);
            if(Physics.OverlapSphere(spawnpos, 3f, mask).Length == 0)
            {
            GameObject temp;
            temp = Instantiate(environment[Random.Range(0, environment.Count)], spawnpos, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), parent);

            environment.Add(temp);
            }
            else
            {
                //i--;
                Debug.Log("Oh no we hit something");
            }
        }
    }
}
