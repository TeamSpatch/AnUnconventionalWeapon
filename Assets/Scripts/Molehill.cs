using UnityEngine;
using System.Collections;

public class Molehill : MonoBehaviour
{
    public float spawnPeriod;
    public int spawnMax;

    int spawnCount;
    float spawnTimer;

    void Start()
    {
        GameObject.Find("Garden").GetComponent<Gardener>().molehillCount += 1;
        spawnTimer = spawnPeriod / 5f * 4f;
    }

    void FixedUpdate()
    {
        if (transform.position.y < 0f) {
            Vector3 pos = transform.position;
            pos.y += Time.fixedDeltaTime;
            if (pos.y > 0) {
                pos.y = 0;
            }
            transform.position = pos;
        } else {
            if (spawnCount < spawnMax) {
                spawnTimer += Time.fixedDeltaTime;
                if (spawnTimer >= spawnPeriod) {
                    GameObject mole = Instantiate(Resources.Load("Mole")) as GameObject;
                    mole.transform.position = transform.position;
                    spawnTimer -= spawnPeriod;
                    spawnCount += 1;
                }
            }
        }
    }
}
