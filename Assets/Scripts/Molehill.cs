using UnityEngine;
using System.Collections;

public class Molehill : MonoBehaviour
{
    public float spawnPeriod;

    float spawnTimer;

    void Start()
    {
        spawnTimer = spawnPeriod / 2;
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
            spawnTimer += Time.fixedDeltaTime;
            if (spawnTimer >= spawnPeriod) {
                GameObject mole = Instantiate(Resources.Load("Mole")) as GameObject;
                mole.transform.position = transform.position;
                spawnTimer -= spawnPeriod;
            }
        }
    }
}
