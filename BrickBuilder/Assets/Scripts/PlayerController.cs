using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    Rigidbody BB_rb;
    bool isGrounded = true;
    bool hasJumped;
    [SerializeField] private float BB_speed = 100f;
    [SerializeField] private float BB_JumpHeight = 500;
    [SerializeField] private float BB_JumpBoost = 500;
    public bool PLAY = true;

    private void Start()
    {
        BB_rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (PLAY)
        {
            Physics();
            Movement();
        }

    }

    void Jump()
    {
        if (isGrounded && !hasJumped)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                BB_rb.AddForce(0, BB_JumpHeight, 0);
                Debug.Log("jump");
                hasJumped = true;
            }
        }

        if (BB_rb.velocity.y >= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (hasJumped)
                {
                    BB_rb.AddForce(0, BB_JumpBoost * Time.deltaTime, 0);
                }
            }
        }
    }
    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 tempVect = new Vector3(h, 0, v);
        tempVect = tempVect.normalized * BB_speed * Time.deltaTime;
        tempVect = transform.TransformDirection(tempVect);
        BB_rb.MovePosition(transform.position + tempVect );

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"))* 10);

        Jump();
    }

    void Physics()
    {
        BB_rb.AddForce(0, -980 * Time.deltaTime, 0);
    }


    //ground check
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
