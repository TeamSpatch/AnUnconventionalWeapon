using UnityEngine;
using System.Collections;

public class Gardener : MonoBehaviour
{
    public Vector2 gardenSize;
    public float tileSize;
    public float voxelSize;
    public float bushPower;
    public float golfPower;
    public float clayPower;
    public float molehillPower;
    [HideInInspector]
    public int killCount = 0;
    [HideInInspector]
    public int sackCount = 0;
    [HideInInspector]
    public int clayCount = 0;
    [HideInInspector]
    public int molehillCount = 0;
    [HideInInspector]
    public int golfCount = 0;
    [HideInInspector]
    public int bushCount = 0;

    void Start()
    {
        for (float row = 0; row < gardenSize.x; row++) {
            for (float col = 0; col < gardenSize.y; col++) {
                int level = Random.Range(3, 5);
                float x = row * tileSize * voxelSize - Mathf.Floor(tileSize / 2f) * voxelSize;
                float y = col * tileSize * voxelSize - Mathf.Floor(tileSize / 2f) * voxelSize;
                for (float i = 0; i < tileSize; i++) {
                    for (float j = 0; j < tileSize; j++) {
                        SpawnVoxel(x + i * voxelSize, y + j * voxelSize, level);
                    }
                }
            }
        }
    }

    void SpawnVoxel(float x, float z, int level)
    {
        GameObject voxel = Instantiate(Resources.Load("Grass")) as GameObject;
        voxel.transform.position = new Vector3(x, voxel.transform.position.y, z);
        voxel.transform.localScale = new Vector3(voxelSize, 0, voxelSize);
        voxel.GetComponent<Grass>().Level = level;
        voxel.transform.parent = transform;
    }

    void Update()
    {
        float good = bushPower * bushCount / (tileSize * tileSize) + golfPower * golfCount / (tileSize * tileSize);
        if (good >= 516f) {
            Application.LoadLevel(4);
        } else {
            float bad = clayPower * clayCount / (tileSize * tileSize) + molehillPower * molehillCount;
            if (bad >= 520f) {
                Application.LoadLevel(3);
            }
        }
    }
}
