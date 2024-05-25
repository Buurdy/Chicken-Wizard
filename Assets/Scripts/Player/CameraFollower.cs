using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target; // The target for the camera to follow
    public float smoothTime = 0.3f; // The time it takes to smooth damp to the target position
    public Vector3 offset = new Vector3(0, 0, -10); // The offset from the target position

    private Vector3 velocity = Vector3.zero; // The current velocity, used by SmoothDamp

    void LateUpdate()
    {
        if (target == null)
            return;

        // Define the target position with the offset
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
