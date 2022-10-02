using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private TopDownControls playerInput;
    private CharacterController characterController;

    private Vector2 inputMove;
    private Vector2 playerVelocity = Vector2.zero;

    public float walkSpeed = 6f;

    private void OnEnable()
    {
        playerInput = new TopDownControls();

        playerInput.Player.Move.performed += e => inputMove = e.ReadValue<Vector2>();
        playerInput.Player.Move.canceled += e => inputMove = Vector2.zero;

        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= e => inputMove = e.ReadValue<Vector2>();

        playerInput.Player.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inputMove != Vector2.zero)
        {
            playerVelocity = inputMove * walkSpeed * Time.deltaTime;
        }
        else
        {
            playerVelocity = Vector2.zero;
        }

        characterController.Move(playerVelocity);

    }
}
