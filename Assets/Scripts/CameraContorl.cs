using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraContorl : MonoBehaviour
{
    //Blueprint

    public GameObject target;
    public float heightOffset = 1.7f;
    public float distance = 12.0f;
    public float OffsetFromWall = 0.1f;
    public float maxDistance = 20f;
    public float minDistance = 0.6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public float yMinLimit = -80f;
    public float yMaxLimit = 80f;
    public float zoomSpeed = 40f;
    public float autoRotationSpeed = 3.0f;
    public float autoZoomSpeed = 5.0f;
    public LayerMask collisionLayers = 1;
    public bool alwaysRotateToRearOfTarget = false;
    public bool allowMouseInputX = true;
    public bool allowMouseInputy = true;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind = false;
    private bool mouseSideButton = false;


    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;
        if (alwaysRotateToRearOfTarget)
        {
            rotateBehind = true;
        }

    }


    // camera is moving after everything is  else has been updated
    void LateUpdate()
    {
        if (allowMouseInputX)
        {
            // GetButton("Toggle Move");
            mouseSideButton = !mouseSideButton;
        }
    }
}
