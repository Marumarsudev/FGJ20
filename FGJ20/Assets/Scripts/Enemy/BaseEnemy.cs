using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{

    public NavMeshAgent meshAgent;

    public GameObject bullet;

    public float travelRadius;
    public float visionRange;
    public float fieldOfView;

    private Vector3 startPos;
    private Vector3 travelpos;

    private float nextMoveTimer;

    private bool moving;
    private bool seeingPlayer = false;

    private Terrain terrain;

    private GameObject player;

    public LayerMask mask;

    public IEnumerator FireBurst(GameObject bulletPrefab, int burstSize, float rateOfFire)
    {
        float bulletDelay = 60 / rateOfFire; 
        // rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
        for (int i = 0; i < burstSize; i++)
        {
            Instantiate(bulletPrefab, transform.forward * 1.5f + transform.position + Vector3.up * 2f, Quaternion.identity); // It would be wise to use the gun barrel's position and rotation to align the bullet to.
            
            yield return new WaitForSeconds(bulletDelay); // wait till the next round
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        nextMoveTimer = Random.Range(2f, 4f);

        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(nextMoveTimer > 0)
            nextMoveTimer -= Time.deltaTime;

        if(nextMoveTimer <= 0 && !moving && !seeingPlayer)
        {

            travelpos = new Vector3(Random.Range(-travelRadius, travelRadius), 0, Random.Range(-travelRadius, travelRadius)) + startPos;

            travelpos.y = terrain.SampleHeight(travelpos);

            meshAgent.SetDestination(travelpos);
            moving = true;
        }

        if(Vector3.Distance(transform.position, travelpos) <= 1f && moving && !seeingPlayer)
        {
            nextMoveTimer = Random.Range(2f, 4f);
            moving = false;
        }

        if(seeingPlayer && nextMoveTimer <= 0)
        {
            nextMoveTimer = Random.Range(2f, 4f);
            StartCoroutine(FireBurst(bullet, 3, 200));
        }

        Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, int.MaxValue);

        if(Vector3.Distance(transform.position, player.transform.position) <= visionRange && hit.collider.tag == "Player")
        {
            Vector3 targetDir = player.transform.position - transform.position;
            if(Vector3.Angle(targetDir, transform.forward) <= fieldOfView || Vector3.Distance(transform.position, player.transform.position) <= 10f)
            {
                meshAgent.SetDestination(player.transform.position);
                seeingPlayer = true;
                moving = false;
            }
        }
        else if(seeingPlayer && Vector3.Distance(transform.position, player.transform.position) >= visionRange * 2f || hit.collider.tag != "Player")
        {
            seeingPlayer = false;
        }
    }
}
