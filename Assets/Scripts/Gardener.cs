using UnityEngine;
using System.Collections;

public class Gardener : MonoBehaviour
{
    public Vector2 gardenSize;
    public float tileSize;
    public float voxelSize;

    void Start()
    {
        for (int x = 0; x < tileSize * voxelSize * gardenSize.x; x++) {
            for (int y = 0; y < tileSize * voxelSize * gardenSize.y; y++) {
                GameObject voxel = Instantiate(Resources.Load("Voxel"), new Vector3(x * voxelSize, 0, y * voxelSize), Quaternion.identity) as GameObject;
                voxel.transform.localScale = new Vector3(voxelSize, 1f / 18f, voxelSize);
                voxel.transform.parent = transform;
            }
        }
    }
}
