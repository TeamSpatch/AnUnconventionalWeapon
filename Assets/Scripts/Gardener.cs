using UnityEngine;
using System.Collections;

public class Gardener : MonoBehaviour
{
    public Vector2 gardenSize;
    public float tileSize;
    public float voxelSize;

    void Start()
    {
        int level = Random.Range(0, 5);
        for (float x = 0; x < tileSize * voxelSize * gardenSize.x; x += voxelSize) {
            for (float y = 0; y < tileSize * voxelSize * gardenSize.y; y += voxelSize) {
                if (x % (tileSize * voxelSize) == 0) {
                    level = Random.Range(0, 5);
                }
                GameObject voxel = Instantiate(Resources.Load("Grass"), new Vector3(x, 0, y), Quaternion.identity) as GameObject;
                voxel.transform.localScale = new Vector3(voxelSize, 0, voxelSize);
                voxel.GetComponent<Grass>().Level = level;
                voxel.transform.parent = transform;
            }
        }
    }
}
