using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{
    private Transform balloonSpawner = null;
    public GameObject balloon;
    public float moveSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        balloonSpawner = transform.Find("BalloonSpawnPoint");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBalloon();
        }
    }

    private void LaunchBalloon()
    {
        GameObject balloonObject = Instantiate(balloon, balloonSpawner.position, Quaternion.identity, balloonSpawner);

        Rigidbody rb = balloonObject.AddComponent<Rigidbody>();

        rb.useGravity = false;
        rb.velocity = moveSpeed * balloonSpawner.up;

        //StartCoroutine(RemoveBalloon_RB(rb, 3.0f));

        new WaitForSeconds(1f);

        Destroy(balloonObject);
    }
}
