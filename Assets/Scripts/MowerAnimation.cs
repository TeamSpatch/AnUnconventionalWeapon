using UnityEngine;
using System.Collections;

public class MowerAnimation : MonoBehaviour
{
    public float standPeriod;
    public float movementPeriod;

    float timer;
    int state;
    MowerMovement movement;

    void Start()
    {
        movement = GetComponent<MowerMovement>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (movement.destination != transform.position) {
            if (timer >= movementPeriod) {
                UpdateAnimation();
                timer -= movementPeriod;
            }
        } else {
            if (timer >= standPeriod) {
                UpdateAnimation();
                timer -= standPeriod;
            }
        }
    }

    void UpdateAnimation()
    {
        if (state == 0) {
            transform.localScale = new Vector3(1, 0.9f, 1);
            Vector3 pos = transform.position;
            pos.y = 0.0f;
            transform.position = pos;
            state = 1;
        } else if (state == 1) {
            transform.localScale = new Vector3(1, 1, 1);
            Vector3 pos = transform.position;
            pos.y = -0.05f;
            transform.position = pos;
            state = 2;
        } else if (state == 2) {
            transform.localScale = new Vector3(1, 0.9f, 1);
            Vector3 pos = transform.position;
            pos.y = 0.0f;
            transform.position = pos;
            state = 3;
        } else {
            transform.localScale = new Vector3(1, 1, 1);
            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;
            state = 0;
        }
    }
}
