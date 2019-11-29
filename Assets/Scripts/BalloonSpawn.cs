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

    private void Start()
    {
        launchTimer = Random.Range(minSpawnTimerRange, maxSpawnTimerRange);
        balloonLifeTime = 7f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        launchTimer -= 0.001f;

        if (launchTimer <= 0)
        {
            launchTimer = Random.Range(minSpawnTimerRange, maxSpawnTimerRange); // 1.5f initially
            /*int rNumber = Random.Range(0, 2);
            Debug.Log(rNumber);
            if (rNumber == 0)
            {
                LaunchBalloon();
            }*/
            LaunchBalloon();
        }
    }

    private void LaunchBalloon()
    {
        GameObject balloonObject = Instantiate(balloon, balloonSpawner.position, Quaternion.identity, balloonSpawner);

        Rigidbody rb = balloonObject.AddComponent<Rigidbody>();

        int rSpeed = 6; //Random.Range(5, 12);

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
