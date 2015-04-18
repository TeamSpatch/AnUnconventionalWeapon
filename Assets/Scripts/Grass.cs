using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour
{
    public float unit;
    public Material[] materials;

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value % 5;
            MeshRenderer renderer = transform.FindChild("Graphics").GetComponent<MeshRenderer>();
            renderer.material = materials[level];
            if (level == 0) {
                transform.localScale = new Vector3(transform.localScale.x, unit, transform.localScale.z);
            } else if (level == 1) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 2, transform.localScale.z);
            } else if (level == 2) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 3, transform.localScale.z);
            } else if (level == 3) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 4, transform.localScale.z);
            } else if (level == 4) {
                transform.localScale = new Vector3(transform.localScale.x, unit * 5, transform.localScale.z);
            }
        }
    }

    int level = 0;
}
