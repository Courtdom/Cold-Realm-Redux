using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{






    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            LevelManager.instance.RespawnOutOfBounds();

        }
    }
}
