using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public Rigidbody Rb;
    public GameObject Player;
    public float horizonatal;
    public float vertical;
    public float moveForce;
    public float jumpForce;
    public bool isGrounded;


    void Update()
    {

        horizonatal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.W))
        {
            Player.GetComponent<Rigidbody>().AddForce(Vector3.forward * moveForce);
            if (Input.GetKeyUp(KeyCode.W))
            {
                Player.GetComponent<Rigidbody>().velocity = (Vector3.zero);

            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            Player.GetComponent<Rigidbody>().AddForce(Vector3.back * moveForce);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Player.GetComponent<Rigidbody>().AddForce(Vector3.right * moveForce);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Player.GetComponent<Rigidbody>().AddForce(Vector3.left * moveForce);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                Player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);

            }

        }








    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Balloon")
        {
            isGrounded = true;

        }

    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Balloon")
        {
            isGrounded = false;
        }

    }


}
