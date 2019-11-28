using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{
    public Transform balloonSpawner;
    public GameObject balloon;

    [SerializeField] private float launchTimer;
    private float balloonLifeTime;

    private void Start()
    {
        launchTimer = 1.5f;
        balloonLifeTime = 7f;
        LaunchBalloon();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        launchTimer -= 0.01f;

        if (launchTimer <= 0)
        {
            launchTimer = 1.5f;
            int rNumber = Random.Range(0, 2);
            Debug.Log(rNumber);
            if(rNumber == 0)
            {
                LaunchBalloon();
            }
        }
    }

    private void LaunchBalloon()
    {
        GameObject balloonObject = Instantiate(balloon, balloonSpawner.position, Quaternion.identity, balloonSpawner);

        Rigidbody rb = balloonObject.AddComponent<Rigidbody>();

        int rSpeed = Random.Range(5, 12);

        rb.useGravity = false;
        rb.velocity = rSpeed * balloonSpawner.up;

        StartCoroutine(RemoveBalloon(balloonObject, balloonLifeTime));
    }

    IEnumerator RemoveBalloon(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(obj);
    }
}
