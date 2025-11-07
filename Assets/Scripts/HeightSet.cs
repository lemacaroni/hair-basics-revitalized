using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSet : MonoBehaviour
{
    public GameControllerScript gc;
    public int heihgt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gc.player.height = heihgt;
        }
    }
}
