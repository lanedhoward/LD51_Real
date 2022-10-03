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
    bool loopRunning = false;

    Coroutine looper;

    public string[] interactionsToReset;
    void Start()
    {
        Debug.Log($"This scene is number {SceneManager.GetActiveScene().buildIndex}");
        //Debug.Log(SceneManager.sceneCountInBuildSettings);

        sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        if(sceneNumber == 4)
        {
            sceneNumber = 1;
        }
        if(!testing)
        {
            looper = StartCoroutine(Wait(loopTime, sceneNumber));
        }


    }

    public void StopLoop()
    {
        if (loopRunning)
        {
            StopCoroutine(looper);
        }
    }

    IEnumerator Wait(float seconds, int i)
    {
        loopRunning = true;
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(scenes[i]);
        loopRunning = false;
    }
}
