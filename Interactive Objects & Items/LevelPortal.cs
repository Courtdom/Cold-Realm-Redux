using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{

    [SerializeField]
    private float levelLoadDelay = 2f;
    [SerializeField]
    private float levelExitSloMo = 0.2f;


    private void Start()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            StartCoroutine(LoadNextLevel());



        }
    }


    

    IEnumerator LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

       

        Time.timeScale = levelExitSloMo;

        yield return new WaitForSecondsRealtime(levelLoadDelay);

        Time.timeScale = 1f;

        Player.instance.SelectStartingPoint();

        SceneManager.LoadScene(currentSceneIndex + 1);

        yield return new WaitForSeconds(1f);


        SceneManager.UnloadSceneAsync(currentSceneIndex);
        yield return new WaitForSeconds (1f);
             
    }

  
}
