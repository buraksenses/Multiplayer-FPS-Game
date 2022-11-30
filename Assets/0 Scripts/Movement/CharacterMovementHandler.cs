using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
   private NetworkCharacterControllerPrototypeCustom _networkCharacterControllerPrototypeCustom;

   private float _cameraRotationX;
   private Camera _localCamera;

   private Vector2 _viewInput;

   private void Awake()
   {
      _networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
      _localCamera = GetComponentInChildren<Camera>();
   }

   private void Update()
   {
      _cameraRotationX += -(_viewInput.y * Time.deltaTime * 200);
      _cameraRotationX = Mathf.Clamp(_cameraRotationX, -90, 90);
      
      _localCamera.transform.localRotation = Quaternion.Euler(_cameraRotationX,0,0);
   }

   public override void FixedUpdateNetwork()
   {
      if (GetInput(out NetworkInputData networkInputData))
      {
         //Rotate the view
         _networkCharacterControllerPrototypeCustom.Rotate(networkInputData.rotationInput);
         
         Vector3 moveDirection = transform.forward * networkInputData.movementInput.y +
                                 transform.right * networkInputData.movementInput.x;
         
         _networkCharacterControllerPrototypeCustom.Move(moveDirection);
         
         //Jump
         if (networkInputData.isJumpPressed)
         {
            _networkCharacterControllerPrototypeCustom.Jump();
         }
      }
   }

   public void SetViewInputVector(Vector2 viewInput)
   {
      this._viewInput = viewInput;
   }
}
