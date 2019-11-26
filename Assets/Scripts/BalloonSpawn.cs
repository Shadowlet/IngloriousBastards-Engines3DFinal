using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawn : MonoBehaviour
{
    public GameObject balloon;
    public Transform spawnPoint;
    public bool popped;
    public float lifeTime;

    public Vector3 heightLimit;

    public float moveSpeed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // As long as the player has not popped it, nor it is above the height limit, the balloon will continue going up
        if(balloon.transform.position.y >= heightLimit.y)
        {
            Debug.Log("test");
            SpawnBalloon();
            Destroy(balloon);
        }
        else
        {
            balloon.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
    }

    public void SpawnBalloon()
    {
        Vector3 spawnLocation = spawnPoint.position;
        Instantiate(balloon, spawnLocation, Quaternion.identity, spawnPoint);
    }
}
