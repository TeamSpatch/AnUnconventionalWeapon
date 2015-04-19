using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    public void OnInstructions()
    {
        Application.LoadLevel(1);
    }

    public void OnStart()
    {
        Application.LoadLevel(2);
    }
}
