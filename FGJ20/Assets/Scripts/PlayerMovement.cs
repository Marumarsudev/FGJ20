using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject playerCam;

    public Transform playerModel;

    public Animator animator;

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

        playerModel.localPosition.Set(0,-0.9f,0);
    }

    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Punch");
        }
        if(Input.GetMouseButtonUp(0))
        {
            animator.ResetTrigger("Punch");
        }

        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        Vector3 movement = playerCam.transform.forward * Input.GetAxis("Vertical") + playerCam.transform.right * Input.GetAxis("Horizontal");;
        movement.y = 0;

        if(movement != Vector3.zero)
        {
            animator.SetBool("Walking", true);
            MovePlayer(movement.normalized);
        }
        else if(body.velocity != Vector3.zero)
        {
            ReduceSpeed();
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        MoveCamera();

        animator.SetFloat("Speed", body.velocity.magnitude);
        animator.SetFloat("Direction", body.velocity.magnitude * Input.GetAxis("Vertical"));
    }

    void MoveCamera()
    {
        
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -89, 70);

        playerCam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        playerModel.eulerAngles = new Vector3(0, yaw, 0);
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
