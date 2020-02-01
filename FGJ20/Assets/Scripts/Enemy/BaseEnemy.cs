using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{

    public NavMeshAgent meshAgent;

    public float travelRadius;
    public float minTravel;
    public float visionRange;
    public float fieldOfView;

    private Vector3 startPos;
    private Vector3 travelpos;

    private float nextMoveTimer;

    private bool moving;

    private Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        nextMoveTimer = Random.Range(2f, 4f);

        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
    }

    // Update is called once per frame
    void Update()
    {
        nextMoveTimer -= Time.deltaTime;

        if(nextMoveTimer <= 0 && !moving)
        {
            //nextMoveTimer = Random.Range(2f, 4f);

            travelpos = new Vector3(Random.Range(minTravel, travelRadius), 0, Random.Range(minTravel, travelRadius)) + startPos;

            travelpos.y = terrain.SampleHeight(travelpos);

            meshAgent.SetDestination(travelpos);
            moving = true;
        }

        if(Vector3.Distance(transform.position, travelpos) <= 1f && moving)
        {
            nextMoveTimer = Random.Range(2f, 4f);
            moving = false;
        }
    }
}
