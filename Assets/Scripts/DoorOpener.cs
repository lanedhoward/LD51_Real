using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] string keyBoolName;
    private bool thingImUsingToMakeTheCodeWork;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        thingImUsingToMakeTheCodeWork = true;
    }

    void Update()
    {
        bool check;
        if(player.interactionsInventory.inventory.TryGetValue(keyBoolName, out check) && check == true)
        {
            if(thingImUsingToMakeTheCodeWork)
            {
                Destroy(this.gameObject);
                thingImUsingToMakeTheCodeWork = false;
            }
        }
        
    }
}
