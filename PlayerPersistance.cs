using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health = " + PlayerPrefs.GetInt("health"));

    }


    public static void SaveData(Player player)
    {
        PlayerPrefs.SetInt("health", player.currentHealth);
        PlayerPrefs.SetInt("xp", player.experiencePoints);
     //   PlayerPrefs.SetFloat("x", player.transform.position.x);
     //   PlayerPrefs.SetFloat("y", player.transform.position.y);
     //   PlayerPrefs.SetFloat("z", player.transform.position.z);

    }





    public static PlayerDataStorage LoadData()
    {
        int health = PlayerPrefs.GetInt("health");
        int xp = PlayerPrefs.GetInt("xp");
      //  float x = PlayerPrefs.GetFloat("x");
       // float y = PlayerPrefs.GetFloat("y");
       // float z = PlayerPrefs.GetFloat("z");



        PlayerDataStorage dataStorage = new PlayerDataStorage()
        {
            HP = health,
            XP = xp,
        };

        return dataStorage;
    }






}
