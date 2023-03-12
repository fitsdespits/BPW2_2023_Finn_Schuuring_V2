using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Standard camera movement.
    public GameObject target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public void Awake()
    {
        transform.position = target.transform.position;
    }
    public void FixedUpdate()
    {
        //Moving camera to the player.
        Vector3 desiredPosition = target.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
