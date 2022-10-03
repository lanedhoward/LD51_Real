using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InteractionsInventory : MonoBehaviour
{
    public Dictionary<string, bool> inventory;

    private Interactable interactable;

    private DialogueManager dialogueManager;

    public void Awake()
    {
        inventory = new Dictionary<string, bool>();
    }
    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public bool CheckForFlag(string s)
    {
        if (inventory.ContainsKey(s))
        {
            if (inventory[s] == true)
            {
                return true;
            }
            
        }
        else
        {
            // dont have the key yet, so add it. but we havent earned the truth yet.
            inventory.Add(s, false);
        }
        
        return false;
    }

    public void SetFlag(string s, bool b)
    {
        if (inventory.ContainsKey(s))
        {
            inventory[s] = b;
        }
        else
        {
            inventory.Add(s, b);
        }
    }

    public bool Interact(Interactable obj)
    {
        if (obj.requiredInteractions.Length > 0)
        {
            foreach (string s in obj.requiredInteractions)
            {
                if (!CheckForFlag(s))
                {
                    if (obj.failureInteractions.Length > 0)
                    {
                        foreach (string j in obj.failureInteractions)
                        {
                            SetFlag(j, true);
                        }
                    }
                    return false; //if missing any of the required flags,
                }
            }
        }

        //has all requirements, so set the results

        if (obj.resultingInteractions.Length > 0)
        {
            foreach (string s in obj.resultingInteractions)
            {
                SetFlag(s, true);
            }
        }
        return true;
    }
    

    public void ReceiveInteractable(Interactable _interactable)
    {
        interactable = _interactable;
    }

    public void ClearInteractable()
    {
        interactable = null;
    }

    public void PressInteract()
    {
        if (!dialogueManager.IsDialogueActive())
        {
            if (interactable != null)
            {
                bool success = Interact(interactable);

                string text;
                if (success)
                {
                    text = interactable.successMessage;
                }
                else
                {
                    text = interactable.failMessage;
                }


                dialogueManager.DisplayText(text);

                //Debug.Log("Interacted" + success);

            }
            else
            {
                dialogueManager.StopDisplay();
                //Debug.Log("Interacted, null");
            }
        }
        else
        {
            dialogueManager.StopDisplay();
        }
    }
}
