using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpeechBubbleScript : MonoBehaviour
{
    public string text = "Sample";
    public float duration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitBubble(string s)
    {
        TMPro.TextMeshPro tmp = GetComponent<TMPro.TextMeshPro>();
        tmp.text = s;
        StartCoroutine(DialogueTimeOut());
    }


    IEnumerator DialogueTimeOut()
    {
        yield return new WaitForSeconds(duration);

        Destroy(this);

    }
}
