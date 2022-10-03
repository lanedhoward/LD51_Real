using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private TopDownControls playerInput;
    private Rigidbody2D characterRigidbody;
    public InteractionsInventory interactionsInventory;

    private SpriteRenderer gun;
    private SpriteRenderer biggerGun;
    private Vector2 inputMove;
    private Vector2 playerVelocity = Vector2.zero;

    public float walkSpeed = 6f;

    private static GameObject instance;

    private void OnEnable()
    {
        

        playerInput = new TopDownControls();

        playerInput.Player.Move.performed += e => inputMove = e.ReadValue<Vector2>();
        playerInput.Player.Move.canceled += e => inputMove = Vector2.zero;

        playerInput.Player.Interact.performed += e => PressInteract();

        playerInput.Player.Enable();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= e => inputMove = e.ReadValue<Vector2>();

        playerInput.Player.Disable();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        characterRigidbody = GetComponent<Rigidbody2D>();
        interactionsInventory = GetComponent<InteractionsInventory>();
        gun = GameObject.Find("Gun").GetComponent<SpriteRenderer>();
        biggerGun = GameObject.Find("BiggerGun").GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (inputMove != Vector2.zero)
        {
            playerVelocity = inputMove * walkSpeed * Time.deltaTime;
        }
        else
        {
            playerVelocity = Vector2.zero;
        }

        //characterController.Move(playerVelocity);
        characterRigidbody.velocity = playerVelocity;

    }

    void Update()
    {
        bool check;
        if(interactionsInventory.inventory.TryGetValue("tookRobbersGun", out check) && check == true)
        {
            gun.enabled = true;
        }
        else if(interactionsInventory.inventory.TryGetValue("tookRobbersGun", out check) && check == false)
        {
            gun.enabled = false;
        }
        if(interactionsInventory.inventory.TryGetValue("hasBiggerGun", out check) && check == true)
        {
            gun.enabled = false;
            biggerGun.enabled = true;
        }
        else if(interactionsInventory.inventory.TryGetValue("hasBiggerGun", out check) && check == false)
        {
            biggerGun.enabled = false;
        }
    }


    private void PressInteract()
    {
        interactionsInventory.PressInteract();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
