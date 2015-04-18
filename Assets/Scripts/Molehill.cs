using UnityEngine;
using System.Collections;

public class Molehill : MonoBehaviour
{
    public float spawnPeriod;

    Vector3 destination;
    float spawnTimer;

    void Start()
    {
        destination = transform.position;
        destination.y = 0f;
        spawnTimer = spawnPeriod;
    }

    void FixedUpdate()
    {
        if (transform.position.y < 0f) {
            transform.position = Vector2.MoveTowards(transform.position, destination, Time.fixedDeltaTime * 1.3f);
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
