using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    public GameObject playerCam;

    public GameObject weapon;
    public GameObject axe;

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    public float speedH = 2f;
    public float speedV = 2f;

    private float yaw = 0;
    private float pitch = 0;

    public KeyCode sprintKey;

    public Collider groundCheck;

    public TextMeshProUGUI infoText;

    private Rigidbody body;

    public Animator animator;

    private bool canJump = true;

    public float damage = 25f;

    void Start()
    {
        infoText.text = "";
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

        if(Input.GetKeyDown(KeyCode.Alpha1) && !weapon.activeInHierarchy)
        {
            if(!axe.activeInHierarchy)
            {
                animator.SetTrigger("takeaxe");
            }
            else
            {
                animator.SetTrigger("putaxe");
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && !axe.activeInHierarchy)
        {
            if(!weapon.activeInHierarchy)
            {
                animator.SetTrigger("takeweapon");
            }
            else
            {
                animator.SetTrigger("putweapon");
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }

        RaycastHit hit;

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("punch");

            if(axe.activeInHierarchy && Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 6f))
            {
                if(hit.collider.GetComponent<BaseEnemy>())
                {
                    hit.collider.GetComponent<HealthComponent>().TakeHealth(damage);
                }
                else if(hit.collider.GetComponent<WorldItem>())
                {
                    hit.collider.GetComponent<WorldItem>().Interact(gameObject);
                }
            }
            else if(weapon.activeInHierarchy && Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 150f))
            {
                if(hit.collider.GetComponent<BaseEnemy>())
                {
                    hit.collider.GetComponent<HealthComponent>().TakeHealth(damage * 1.75f);
                }
            }
            else if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 6f))
            {
                if(hit.collider.GetComponent<WorldItem>())
                {
                    hit.collider.GetComponent<WorldItem>().Interact(gameObject);
                }
            }
        }

        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, 6f))
        {
            if(hit.collider.GetComponent<InfoText>())
            {
                infoText.text = hit.collider.GetComponent<InfoText>().infoText;
            }
            else
            {
                infoText.text = "";
            }
        }
        else
        {
            infoText.text = "";
        }

        Vector3 movement = playerCam.transform.forward * Input.GetAxis("Vertical") + playerCam.transform.right * Input.GetAxis("Horizontal");;
        movement.y = 0;

        if(movement != Vector3.zero)
        {
            animator.SetBool("Moving", true);
            MovePlayer(movement.normalized);
        }
        else if(body.velocity != Vector3.zero)
        {
            ReduceSpeed();
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        MoveCamera();
        animator.SetFloat("Speed", body.velocity.magnitude);
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
