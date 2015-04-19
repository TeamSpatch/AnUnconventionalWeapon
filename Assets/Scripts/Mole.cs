using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour
{
    public float flySpeed;
    public float ejectingDuration;
    public float fellDuration;
    public float satDuration;
    public float standDuration;
    public float digPeriod;
    public float digCount;

    enum State
    {
        Ejected,
        Falling,
        Fell,
        Sat,
        Stand,
        DigOne,
        DigTwo,
    }

    State state;
    float timer;
    int count;
    Vector3 destination;

    void Start()
    {
        state = State.Ejected;
        transform.rotation = Quaternion.Euler(0f, Random.Range(0, 4) * 90f, 0f);
    }

    void FixedUpdate()
    {
        if (state == State.Ejected) {
            DoEjecting();
        } else if (state == State.Falling) {
            DoFalling();
        } else if (state == State.Fell) {
            DoFell();
        } else if (state == State.Sat) {
            DoSat();
        } else if (state == State.Stand) {
            DoStand();
        } else if (state == State.DigOne) {
            DoDigOne();
        } else if (state == State.DigTwo) {
            DoDigTwo();
        }
    }

    void ChangeModel()
    {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (state == State.Ejected) {
            transform.FindChild("Ejected").gameObject.SetActive(true);
        } else if (state == State.Falling) {
            transform.FindChild("Falling").gameObject.SetActive(true);
        } else if (state == State.Fell) {
            transform.FindChild("Fell").gameObject.SetActive(true);
        } else if (state == State.Sat) {
            transform.FindChild("Sat").gameObject.SetActive(true);
        } else if (state == State.Stand) {
            transform.FindChild("Stand").gameObject.SetActive(true);
        } else if (state == State.DigOne) {
            transform.FindChild("DigOne").gameObject.SetActive(true);
        } else if (state == State.DigTwo) {
            transform.FindChild("DigTwo").gameObject.SetActive(true);
        }
    }

    void DoEjecting()
    {
        timer += Time.fixedDeltaTime;
        Vector3 pos = transform.position;
        pos.y += Time.fixedDeltaTime * flySpeed;
        transform.position = pos;
        if (timer >= ejectingDuration) {
            state = State.Falling;
            destination = GameObject.Find("Garden").GetComponent<MoleMaster>().GetFreeTile();
            pos = destination;
            pos.y = flySpeed;
            transform.position = pos;
            ChangeModel();
        }
    }

    void DoFalling()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.fixedDeltaTime * flySpeed);
        if (transform.position.y == 0) {
            state = State.Fell;
            timer = 0f;
            ChangeModel();
        }
    }

    void DoFell()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= fellDuration) {
            state = State.Sat;
            timer = 0f;
            ChangeModel();
        }
    }

    void DoSat()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= satDuration) {
            state = State.Stand;
            timer = 0f;
            ChangeModel();
        }
    }

    void DoStand()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= standDuration) {
            state = State.DigOne;
            timer = 0f;
            count = 0;
            Vector3 pos = transform.position;
            pos.y = -0.1f * digCount;
            ChangeModel();
        }
    }

    void DoDigOne()
    {
        timer += Time.fixedDeltaTime;
        Vector3 pos = transform.position;
        pos.y = -0.1f * digCount + count * 0.1f + timer / digPeriod * 0.1f;
        transform.position = pos;
        if (timer >= digPeriod) {
            state = State.DigTwo;
            timer = 0f;
            count++;
            if (count >= digCount) {
                GameObject molehill = Instantiate(Resources.Load("Molehill")) as GameObject;
                molehill.transform.position = transform.position;
                molehill.transform.rotation = transform.rotation;
                Destroy(gameObject);
            } else {
                ChangeModel();
            }
        }
    }

    void DoDigTwo()
    {
        timer += Time.fixedDeltaTime;
        Vector3 pos = transform.position;
        pos.y = -0.1f * digCount + count * 0.1f + timer / digPeriod * 0.1f;
        transform.position = pos;
        if (timer >= digPeriod) {
            state = State.DigOne;
            timer = 0f;
            count++;
            if (count >= digCount) {
                GameObject molehill = Instantiate(Resources.Load("Molehill")) as GameObject;
                molehill.transform.position = transform.position;
                molehill.transform.rotation = transform.rotation;
                Destroy(gameObject);
            } else {
                ChangeModel();
            }
        }
    }
}
