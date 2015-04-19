using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour
{
    public float heightUnit;
    public Material[] materials;

    MeshRenderer rend;

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            Gardener gardener = GameObject.Find("Garden").GetComponent<Gardener>();
            if (level == 0) {
                gardener.clayCount -= 1;
            } else if (level == 2) {
                gardener.golfCount -= 1;
            } else if (level == 3) {
                gardener.bushCount -= 1;
            }
            if (value < 0) {
                level = 0;
            } else {
                level = value % 5;
            }
            if (rend == null) {
                rend = transform.GetComponent<MeshRenderer>();
            }
            rend.material = materials[level];
            if (level == 0) {
                transform.localScale = new Vector3(transform.localScale.x, heightUnit, transform.localScale.z);
                gardener.clayCount += 1;
            } else if (level == 1) {
                transform.localScale = new Vector3(transform.localScale.x, heightUnit * 2, transform.localScale.z);
            } else if (level == 2) {
                transform.localScale = new Vector3(transform.localScale.x, heightUnit * 3, transform.localScale.z);
                gardener.golfCount += 1;
            } else if (level == 3) {
                transform.localScale = new Vector3(transform.localScale.x, heightUnit * 4, transform.localScale.z);
                gardener.bushCount += 1;
            } else if (level == 4) {
                transform.localScale = new Vector3(transform.localScale.x, heightUnit * 5, transform.localScale.z);
            }
            transform.position = new Vector3(transform.position.x, transform.localScale.y / 2f, transform.position.z);
        }
    }

    int level = -1;
}
