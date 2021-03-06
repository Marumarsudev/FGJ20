﻿using System.Collections;
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
 
        for(int i = 0; i < environmentcount; i++)
        {
            Vector3 spawnpos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + transform.position;

            spawnpos.y = terrain.SampleHeight(spawnpos);
            if(Physics.OverlapSphere(spawnpos, 3f, mask).Length == 0)
            {
            GameObject temp;
            int rand = Random.Range(0, environment.Count);
            //Quaternion.Euler(new Vector3(environment[rand].transform.localRotation.x, Random.Range(0, 360), environment[rand].transform.localRotation.z))
            temp = Instantiate(environment[rand], spawnpos, environment[rand].transform.rotation, parent);

            temp.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0) + temp.transform.rotation.eulerAngles);

            environment.Add(temp);
            }
            else
            {
                //i--;
                Debug.Log("Oh no we hit something");
            }
        }
    }

    public void RemoveFromEnemies(GameObject obj)
    {
        enemies.Remove(obj);
    }

    void Update()
    {
        if (enemies.Count < enemycount)
        {
            Vector3 spawnpos;
            spawnpos = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)) + transform.position;

            spawnpos.y = terrain.SampleHeight(spawnpos);

            if(Physics.OverlapSphere(spawnpos, 3f, mask).Length  == 0)
            {
                GameObject temp;
                temp = Instantiate(enemies[Random.Range(0, enemies.Count)], spawnpos, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));

                temp.GetComponent<RemoveEnemyFromListEvent>().spawner = this as ObjectSpawner;

                enemies.Add(temp);
            }
            else
            {
                //i--;
                Debug.Log("Oh no we hit something");
            }
        }
    }
}
