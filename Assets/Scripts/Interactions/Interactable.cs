using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    
    public String[] requiredInteractions;

    public String[] resultingInteractions;

    public string successMessage;
    public string failMessage;


    public bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player in range");
            other.SendMessage("ReceiveInteractable", this);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("ClearInteractable");
            //Debug.Log("Player left range");
            playerInRange = false;
        }
    }
}
