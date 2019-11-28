using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{
    public Transform balloonSpawner;
    public GameObject balloon;

    [SerializeField] private float launchTimer;
    private float balloonLifeTime;

    private float minSpawnTimerRange = 0.12f;
    private float maxSpawnTimerRange = 0.86f;
    private float launchTime;

    private void Start()
    {
        launchTimer = Random.Range(minSpawnTimerRange, maxSpawnTimerRange);
        balloonLifeTime = 7f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Every time the launchTimer reaches 0, reset the timer
        launchTimer -= 0.01f;
        if (launchTimer <= 0)
        {
            launchTimer = 1.5f;

            // Set a random number from 0 to 1, if its 0 then launch balloon, otherwise do nothing
            int rNumber = Random.Range(0, 5);
            if (rNumber == 0)
                launchTimer -= 0.001f;

            if (launchTimer <= 0)
            {
                launchTimer = Random.Range(minSpawnTimerRange, maxSpawnTimerRange); // 1.5f initially
                {
                    // Spawn balloon and add rigidbody in each spawner
                    GameObject balloonObject = Instantiate(balloon, balloonSpawner.position, Quaternion.identity, balloonSpawner);
                    Rigidbody rb = balloonObject.AddComponent<Rigidbody>();

                    int Speed = 7;  //Set speed, for development purposes
                    // Define an initial speed from 5 to 11
                    int rSpeed = Random.Range(5, 12);

                    rb.useGravity = false;
                    rb.velocity = Speed * balloonSpawner.up;

                    // After 7 seconds, remove balloon object
                    StartCoroutine(RemoveBalloon(balloonObject, balloonLifeTime));
                }

                IEnumerator RemoveBalloon(GameObject obj, float delay)
                {
                    yield return new WaitForSeconds(delay);

                    Destroy(obj);
                }
            }
        }
    }
}