using UnityEngine;
using System.Collections;

public class MowerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float turningRate;

    Vector3 previousDirection;

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 targetPosition = transform.position + direction * moveSpeed * Time.fixedDeltaTime;
        transform.position = targetPosition;
        if (direction.magnitude >= 0.2) {
            previousDirection = direction;
        }
        float y = Mathf.Atan2(previousDirection.x, previousDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningRate * Time.fixedDeltaTime);
    }
}
