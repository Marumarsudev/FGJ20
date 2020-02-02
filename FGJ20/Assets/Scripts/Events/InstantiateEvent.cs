using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEvent : BaseEvent
{
    
    public GameObject go;

    public Vector3 spawnOffset;

    public Vector3 spawnpos;
    public Vector3 spawnrot;

    public bool useTransformPos;
    public bool usePrefabRotation;

    public int spawnAmount;

    void Start()
    {
        if(useTransformPos)
        {
            spawnpos = transform.position;
        }

        if(usePrefabRotation)
        {
            spawnrot = go.transform.rotation.eulerAngles;
        }
    }

    void Update()
    {
        if(useTransformPos)
        {
            spawnpos = transform.position;
        }
    }

    public override void CallEvent(GameObject interactor = null)
    {
        Debug.Log("transform: " + transform.position);
        Debug.Log(spawnpos);
        for(int i = 0; i < spawnAmount; i++)
            Instantiate(go, spawnpos + (spawnOffset * i), Quaternion.Euler(spawnrot));
    }
}
