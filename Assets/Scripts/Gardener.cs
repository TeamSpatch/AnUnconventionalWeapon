using UnityEngine;
using System.Collections;

public class Gardener : MonoBehaviour
{
    public Vector2 gardenSize;
    public float tileSize;
    public float voxelSize;

    void Start()
    {
        for (float x = 0; x < gardenSize.x; x++) {
            for (float y = 0; y < gardenSize.y; y++) {
                int level = Random.Range(2, 5);
                for (float i = 0; i < tileSize; i++) {
                    for (float j = 0; j < tileSize; j++) {
                        SpawnVoxel(x * tileSize * voxelSize + i * voxelSize, y * tileSize * voxelSize + j * voxelSize, level);
                    }
                }
            }
        }
    }

    void SpawnVoxel(float x, float y, int level)
    {
        GameObject voxel = Instantiate(Resources.Load("Grass"), new Vector3(x, 0, y), Quaternion.identity) as GameObject;
        voxel.transform.localScale = new Vector3(voxelSize, 0, voxelSize);
        voxel.GetComponent<Grass>().Level = level;
        voxel.transform.parent = transform;
    }
}
