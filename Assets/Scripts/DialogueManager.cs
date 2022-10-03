using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    public float dialogueTimer = 1f;
    private bool dialogueTimerRunning = false;

    public bool IsDialogueActive()
    {
        return dialogueBox.activeInHierarchy;
    }
    public void DisplayText(string s)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = s;
        if (dialogueTimerRunning)
        {
            StopCoroutine(DialogueTimeOut());
        }
        StartCoroutine(DialogueTimeOut());
    }
    public void StopDisplay()
    {
        dialogueBox.SetActive(false);
    }

    IEnumerator DialogueTimeOut()
    {
        dialogueTimerRunning = true;
        yield return new WaitForSeconds(dialogueTimer);

        if (IsDialogueActive())
        {
            StopDisplay();
        }
        dialogueTimerRunning = false;

    }
}
