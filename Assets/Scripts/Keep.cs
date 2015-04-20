using UnityEngine;
using System.Collections;

public class Keep : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
