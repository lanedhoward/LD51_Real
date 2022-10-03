using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberScript : MonoBehaviour
{
    public float speed = 20f;
    public bool moving = true;

    public GameObject speechBubble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);

            if (transform.position.y > 1f)
            {
                transform.position = new Vector3(transform.position.x, 1f);
                speechBubble.SetActive(true);
            }
        }
    }
}
