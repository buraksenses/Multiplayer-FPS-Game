using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
   private NetworkCharacterControllerPrototypeCustom _networkCharacterControllerPrototypeCustom;
   private WeaponHandler _weaponHandler;

   private void Awake()
   {
      _networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
      _weaponHandler = GetComponent<WeaponHandler>();
   }

   public override void FixedUpdateNetwork()
   {
      if (GetInput(out NetworkInputData networkInputData))
      { 
         //Rotate the transform according to the client aim vector
        var transform1 = transform;
        transform1.forward = networkInputData.aimForwardVector;
        
        //Cancel out rotation on X axis as we don't want our character to tilt
        Quaternion rotation = transform1.rotation;
        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
        transform1.rotation = rotation;
        
        //Move 
         Vector3 moveDirection = transform1.forward * networkInputData.movementInput.y +
                                 transform1.right * networkInputData.movementInput.x;
         
         _networkCharacterControllerPrototypeCustom.Move(moveDirection);
         
         //Jump
         if (networkInputData.isJumpPressed)
         {
            _networkCharacterControllerPrototypeCustom.Jump();
         }
         
         //Fire
         if(networkInputData.isFireButtonPressed)
            _weaponHandler.Fire(networkInputData.aimForwardVector);
         
         //Check Fall Respawn
         CheckFallRespawn();
      }
   }

   private void CheckFallRespawn()
   {
      if (transform.position.y < -12)
         transform.position = Utils.GetRandomSpawnPoint();
   }
}
