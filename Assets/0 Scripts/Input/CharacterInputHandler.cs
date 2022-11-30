using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;
    public bool isJumpButtonPressed = false;

    private CharacterMovementHandler _characterMovementHandler;

    private void Awake()
    {
        _characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //view input
        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y");

        _characterMovementHandler.SetViewInputVector(viewInputVector);
        
        //move input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        isJumpButtonPressed = Input.GetButtonDown("Jump");
        
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //move data
        networkInputData.movementInput = moveInputVector;
        
        //view data
        networkInputData.rotationInput = viewInputVector.x;
        
        //jump data
        networkInputData.isJumpPressed = isJumpButtonPressed;
        
        return networkInputData;
    }
}
