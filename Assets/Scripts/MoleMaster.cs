using UnityEngine;
using System.Collections;

public class MoleMaster : MonoBehaviour
{
    public float molehillPeriod;

    Transform player;
    Gardener gardener;
    float molehillTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gardener = GetComponent<Gardener>();
    }

    void Update()
    {
        molehillTimer += Time.deltaTime;
        if (molehillTimer >= molehillPeriod) {
            Vector3 pos = GetFreeTile();
            pos.y = -0.6f;
            Instantiate(Resources.Load("Molehill"), pos, Quaternion.Euler(0f, Random.Range(0, 4) * 90f, 0f));
            gardener.molehillCount += 1;
            molehillTimer = molehillTimer - molehillPeriod;
        }
    }

    public Vector3 GetFreeTile()
    {
        int turn = 0;
        Vector3 pos = new Vector3();
        GameObject[] moles = GameObject.FindGameObjectsWithTag("Mole");
        while (turn < 20) {
            pos.x = Random.Range(0, (int)gardener.gardenSize.x);
            pos.z = Random.Range(0, (int)gardener.gardenSize.y);
            pos = pos * gardener.voxelSize * gardener.tileSize;
            if (Vector3.Distance(pos, player.position) >= gardener.tileSize * gardener.voxelSize) {
                bool ok = true;
                foreach (GameObject mole in moles) {
                    if (Vector3.Distance(pos, mole.transform.position) < gardener.tileSize * gardener.voxelSize * 2) {
                        ok = false;
                        break;
                    }
                }
                if (ok == true) {
                    break;
                }
            }
            ++turn;
        }
        return pos;
    }
}
