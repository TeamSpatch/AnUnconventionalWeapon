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
            GameObject molehill = Instantiate(Resources.Load("Molehill")) as GameObject;
            pos.y = molehill.transform.position.y;
            molehill.transform.position = pos;
            molehillTimer = molehillTimer - molehillPeriod;
        }
    }

    Vector3 GetFreeTile()
    {
        Vector3 pos = new Vector3();
        GameObject[] moles = GameObject.FindGameObjectsWithTag("Mole");
        while (true) {
            pos.x = Random.Range(0, (int)gardener.gardenSize.x);
            pos.z = Random.Range(0, (int)gardener.gardenSize.y);
            pos = pos * gardener.voxelSize * gardener.tileSize;
            if (Vector3.Distance(pos, player.position) >= gardener.tileSize * gardener.voxelSize * 2) {
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
        }
        return pos;
    }
}
