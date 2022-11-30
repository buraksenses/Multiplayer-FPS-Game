using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
   private NetworkCharacterControllerPrototypeCustom _networkCharacterControllerPrototypeCustom;

   private void Awake()
   {
      _networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
   }
}
