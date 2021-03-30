using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{

    public static SaveHandler Instance { get; private set; }


   
    private Player player;
 

    string playerHealth = "playerHealth";
    string expPoints = "expPoints";

    int hp, exp;

    private void Awake()
    {
        player = FindObjectOfType<Player>();


        Instance = this;

    }




    // Start is called before the first frame update
    void Start()
    {

        //player.SetPlayerPrefs(hp, exp);






    }

    // Update is called once per frame
    void Update()
    {
    }
}
