using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody Rb;

    public float moveForce;
    public float dashForce;
    public float jumpForce;

    public bool isGrounded;
    public bool isDashing;
    public bool canDash;

    public ScoreManager scores;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }

            if (!isGrounded && canDash)
            {
                isDashing = true;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            canDash = true;
        }

        DoMovement();
        DoDash();
        StopMovement();
    }

    void DoMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Rb.AddForce(Vector3.forward * moveForce);

            if (Rb.velocity.z > (Vector3.forward.z * moveForce))
            {
                Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y, (Vector3.forward.z * moveForce));
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            Rb.AddForce(Vector3.back * moveForce);

            if (Rb.velocity.z < (Vector3.forward.z * moveForce))
            {
                Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y, (Vector3.back.z * moveForce));
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            Rb.AddForce(Vector3.right * moveForce);

            if (Rb.velocity.x > (Vector3.right.x * moveForce))
            {
                Rb.velocity = new Vector3((Vector3.right.x * moveForce), Rb.velocity.y, Rb.velocity.z);
                // new Vector3(x, y, z);
            }

        }

        if (Input.GetKey(KeyCode.A))
        {
            Rb.AddForce(Vector3.left * moveForce);

            if (Rb.velocity.x < (Vector3.left.x * moveForce))
            {
                Rb.velocity = new Vector3((Vector3.left.x * moveForce), Rb.velocity.y, Rb.velocity.z);
            }
        }
    }

    void DoDash()
    {
        if (isDashing)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Rb.AddForce(Vector3.forward * dashForce);
            }

            if (Input.GetKey(KeyCode.S))
            {
                Rb.AddForce(Vector3.back * dashForce);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Rb.AddForce(Vector3.right * dashForce);
            }

            if (Input.GetKey(KeyCode.A))
            {
                Rb.AddForce(Vector3.left * dashForce);
            }
        }
    }

    void Jump()
    {
        Rb.AddForce(Vector3.up * jumpForce);
    }


    void StopMovement()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            Rb.velocity = new Vector3(Rb.velocity.x, Rb.velocity.y, 0.0f);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            Rb.velocity = new Vector3(0.0f, Rb.velocity.y, Rb.velocity.z);
        }


    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Balloon")
        {
            isGrounded = true;
            isDashing = false;
            canDash = false;

        }

    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Balloon")
        {
            isGrounded = false;
            // canDash = true;
            scores.balloonCount++;
            //Debug.Log(scores.balloonCount);
        }
    }


}
