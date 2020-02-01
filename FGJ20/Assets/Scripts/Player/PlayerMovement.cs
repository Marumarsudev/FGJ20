using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject playerCam;

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    public float speedH = 2f;
    public float speedV = 2f;

    private float yaw = 0;
    private float pitch = 0;

    public KeyCode sprintKey;

    public Collider groundCheck;

    private Rigidbody body;

    private bool canJump = true;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void LateUpdate()
    {

        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 3f))
            {
                if(hit.collider.GetComponent<WorldItem>())
                {
                    hit.collider.GetComponent<WorldItem>().Interact(gameObject);
                }
            }
        }

        Vector3 movement = playerCam.transform.forward * Input.GetAxis("Vertical") + playerCam.transform.right * Input.GetAxis("Horizontal");;
        movement.y = 0;

        if(movement != Vector3.zero)
        {
            MovePlayer(movement.normalized);
        }
        else if(body.velocity != Vector3.zero)
        {
            ReduceSpeed();
        }
        MoveCamera();

    }

    void MoveCamera()
    {
        
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -89, 70);

        playerCam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void ReduceSpeed()
    {
        body.AddForce(body.velocity * -0.99f);
    }

    void Jump()
    {
        canJump = false;
        body.AddForce(new Vector3(0,jumpForce,0), ForceMode.Impulse);
    }

    void MovePlayer(Vector3 dir)
    {
        float speed = walkSpeed;
        if(Input.GetKey(sprintKey))
        {
            speed = runSpeed;
        }

        if(body.velocity.magnitude < speed)
            body.AddForce(dir * speed, ForceMode.Impulse);
        else
            body.velocity = new Vector3(dir.x * speed, body.velocity.y, dir.z * speed);
    }

    void OnTriggerEnter(Collider col)
    {
        canJump = true;
    }
}
