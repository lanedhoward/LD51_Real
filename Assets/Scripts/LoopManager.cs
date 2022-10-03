using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    [SerializeField] float loopTime;
    [SerializeField] string[] scenes;
    [SerializeField] bool testing;
    private int sceneNumber;
    void Start()
    {
        Debug.Log($"This scene is number {SceneManager.GetActiveScene().buildIndex}");
        //Debug.Log(SceneManager.sceneCountInBuildSettings);

        sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        if(sceneNumber == SceneManager.sceneCountInBuildSettings)
        {
            sceneNumber = 0;
        }
        if(!testing)
        {
            StartCoroutine(Wait(loopTime, sceneNumber));
        }
    }

    IEnumerator Wait(float seconds, int i)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("done");
        SceneManager.LoadScene(scenes[i]);
    }
}
