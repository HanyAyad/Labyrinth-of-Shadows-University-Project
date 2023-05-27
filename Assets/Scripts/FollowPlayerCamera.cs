using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour 
{
    public Transform target;
    public float smoothSpeed = 10.0f;
    public float distanceFromTarget = 3.0f;
    public float heightOffset = 2.0f;

    void LateUpdate() 
    {
        // Calculate the desired position of the camera
        Vector3 desiredPosition = target.position - target.forward * distanceFromTarget + target.up * heightOffset;

        // Calculate a rotation that looks at the target
        Quaternion lookRotation = Quaternion.LookRotation(target.position - desiredPosition, target.up);

        // Smoothly move the camera to the desired position and rotation
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, smoothSpeed * Time.deltaTime);
    }
}
