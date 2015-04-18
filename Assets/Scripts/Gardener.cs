using UnityEngine;
using System.Collections;

public class Gardener : MonoBehaviour
{
    public Vector2 size;
    public float unitSize;

    void Start()
    {
        for (int x = 0; x < unitSize * size.x; x++) {
            for (int y = 0; y < unitSize * size.y; y++) {
                GameObject voxel = Instantiate(Resources.Load("voxel"), new Vector3(x * 1f / unitSize, 0, y * 1f / unitSize), Quaternion.identity) as GameObject;
                voxel.transform.localScale = new Vector3(1f / unitSize, 1f / 18f, 1f / unitSize);
                voxel.transform.parent = transform;
            }
        }
    }
}
