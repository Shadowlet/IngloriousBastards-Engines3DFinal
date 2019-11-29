using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{
    public Transform balloonSpawner;
    public GameObject balloon;

    [SerializeField] private float launchTimer;
    private float balloonLifeTime;

    private float minSpawnTimerRange = 1.3f;
    private float maxSpawnTimerRange = 2.8f;

    private void Start()
    {
        launchTimer = Random.Range(minSpawnTimerRange, maxSpawnTimerRange);
        balloonLifeTime = 7f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        launchTimer -= 0.01f;
        if (launchTimer <= 0)
        {
            int rNumber = Random.Range(0, 5);

            if(rNumber == 0)
            {
                LaunchBalloon();
            }
            launchTimer = Random.Range(minSpawnTimerRange, maxSpawnTimerRange);
        }
    }
    private void LaunchBalloon()
    {
        // Spawn balloon and add rigidbody in each spawner
        GameObject balloonObject = Instantiate(balloon, balloonSpawner.position, Quaternion.identity, balloonSpawner);
        Rigidbody rb = balloonObject.AddComponent<Rigidbody>();

        int Speed = 7;  //Set speed, for development purposes
        // Define an initial speed from 4 to 9
        int rSpeed = Random.Range(4, 10);

        rb.useGravity = false;
        rb.velocity = rSpeed * balloonSpawner.up;

        // After 7 seconds, remove balloon object
        StartCoroutine(RemoveBalloon(balloonObject, balloonLifeTime));
    }
                    
    IEnumerator RemoveBalloon(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(obj);
    }
}