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
    public bool isFireButtonPressed = false;
    
    private LocalCameraHandler _localCameraHandler;
    private CharacterMovementHandler _characterMovementHandler;

    private void Awake()
    {
        _localCameraHandler = GetComponentInChildren<LocalCameraHandler>();
        _characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(!_characterMovementHandler.Object.HasInputAuthority)
            return;
        
        //view input
        viewInputVector.x = Input.GetAxis("Mouse X") * 20;
        viewInputVector.y = Input.GetAxis("Mouse Y");

        //move input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        //Jump input
        if(Input.GetButtonDown("Jump"))
            isJumpButtonPressed = true;

        if (Input.GetButtonDown("Fire1"))
            isFireButtonPressed = true;
        
        //Set View
        _localCameraHandler.SetViewInputVector(viewInputVector);
        
    }

    public NetworkInputData GetNetworkInput()
    {
        var networkInputData = new NetworkInputData
        {
            //move data
            movementInput = moveInputVector,
            //view data
            aimForwardVector = _localCameraHandler.transform.forward,
            //jump data
            isJumpPressed = isJumpButtonPressed,
            //Fire data
            isFireButtonPressed = isFireButtonPressed
        };

        //Reset variables 
        isJumpButtonPressed = false;
        isFireButtonPressed = false;
        
        return networkInputData;
    }
}
