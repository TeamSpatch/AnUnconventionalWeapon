using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    Vector3 offset;
    Gardener gardener;

    void Start()
    {
        gardener = GameObject.Find("Garden").GetComponent<Gardener>();
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 pos = target.position + offset;
        if (pos.x < 5 * gardener.voxelSize * gardener.tileSize) {
            pos.x = 5 * gardener.voxelSize * gardener.tileSize;
        } else if (pos.x > (gardener.gardenSize.x - 5) * gardener.voxelSize * gardener.tileSize) {
            pos.x = (gardener.gardenSize.x - 5) * gardener.voxelSize * gardener.tileSize;
        }
        if (pos.z < -6 * gardener.voxelSize * gardener.tileSize) {
            pos.z = -6 * gardener.voxelSize * gardener.tileSize;
        } else if (pos.z > (gardener.gardenSize.y - 13) * gardener.voxelSize * gardener.tileSize) {
            pos.z = (gardener.gardenSize.y - 13) * gardener.voxelSize * gardener.tileSize;
        }
        transform.position = pos;
    }
}
