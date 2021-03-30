using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{


    public static LevelManager instance;

    public float waitToRespawn;





    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }
    public void RespawnOutOfBounds()
    {
        StartCoroutine(RespawnOOB());
    }

    private IEnumerator RespawnCo()
    {

       // Player.instance.DeadHologram();

        yield return new WaitForSeconds(waitToRespawn);


       Player.instance.transform.position = CheckpointController.instance.spawnPoint;
       Player.instance.playerCombat.Respawn();

    }
    private IEnumerator RespawnOOB()
    {

        // Player.instance.DeadHologram();

        yield return new WaitForSeconds(waitToRespawn);


        Player.instance.transform.position = CheckpointController.instance.spawnPoint;
        Player.instance.playerCombat.RespawnLevelBoundary();

    }
}
