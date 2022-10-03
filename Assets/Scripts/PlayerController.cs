using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

    private TMPro.TextMeshProUGUI hudTimer;
    private string startTime;
    private float currentTime;

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
            startTime = System.DateTime.Now.ToString("HH:mm");
            currentTime = 0;
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
        currentTime += Time.deltaTime;
        DisplayTime();


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
        var textsList = FindObjectsOfType<TMPro.TextMeshProUGUI>();
        foreach (var i in textsList)
        {
            if (i.CompareTag("hudTimer"))
            {
                hudTimer = i;
                break;
            }
        }
        currentTime = 0;
    }

    private void DisplayTime()
    {
        string currentSeconds = Mathf.FloorToInt(currentTime).ToString().PadLeft(2, '0');
        string display = startTime + ":" + currentSeconds; //string.Format("{0:D2}",  currentSeconds);

        hudTimer.text = display;
    }
}
