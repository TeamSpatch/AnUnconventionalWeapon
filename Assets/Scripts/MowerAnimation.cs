using UnityEngine;
using System.Collections;

public class MowerAnimation : MonoBehaviour
{
    public float standPeriod;
    public float movementPeriod;

    float timer;
    MowerMovement movement;

    void Start()
    {
        timer = 0;
        movement = GetComponent<MowerMovement>();
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (Vector3.Distance(movement.destination, transform.position) >= 0.1f) {
            DoAnim(movementPeriod);
        } else {
            DoAnim(standPeriod);
        }
    }

    void DoAnim(float period)
    {
        if (timer >= period * 4f) {
            timer = 0f;
        } else if (timer >= period * 3f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 pos = transform.position;
            pos.y = 0f;
            transform.position = pos;
        } else if (timer >= period * 2f) {
            transform.localScale = new Vector3(1f, 0.9f, 1f);
            Vector3 pos = transform.position;
            pos.y = 0.03f;
            transform.position = pos;
        } else if (timer >= period) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Vector3 pos = transform.position;
            pos.y = 0f;
            transform.position = pos;
        } else {
            transform.localScale = new Vector3(1f, 0.9f, 1f);
            Vector3 pos = transform.position;
            pos.y = -0.03f;
            transform.position = pos;
        }
    }
}
