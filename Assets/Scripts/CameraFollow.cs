using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The character to follow
    public float smoothSpeed = 0.125f; // Speed at which the camera follows
    public Vector3 offset; // Offset from the target

    void LateUpdate()
    {
        if (target == null)
            return;

        // Desired position is the target's position plus the offset
        Vector3 desiredPosition = target.position + offset;
        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Update the camera's position
        transform.position = smoothedPosition;

        // Optionally, make the camera look at the target
        transform.LookAt(target);
    }
}
