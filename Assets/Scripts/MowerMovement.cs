using UnityEngine;
using System.Collections;

public class MowerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float turningRate;

    Vector3 destination;
    Vector3 direction;
    Gardener gardener;

    void Start()
    {
        gardener = GameObject.Find("Garden").GetComponent<Gardener>();
        destination = transform.position;
        destination.y = 0;
    }

    void FixedUpdate()
    {
        if (destination.x == transform.position.x && destination.z == transform.position.z) {
            if (Input.GetAxisRaw("Horizontal") != 0) {
                destination.x = transform.position.x + gardener.tileSize * gardener.voxelSize * Input.GetAxisRaw("Horizontal");
                direction = destination - transform.position;
            } else if (Input.GetAxisRaw("Vertical") != 0) {
                destination.z = transform.position.z + gardener.tileSize * gardener.voxelSize * Input.GetAxisRaw("Vertical");
                direction = destination - transform.position;
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * moveSpeed);
        }
        float y = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningRate * Time.fixedDeltaTime);
    }
}
