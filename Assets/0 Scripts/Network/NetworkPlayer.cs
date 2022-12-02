using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour,IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    public Transform playerModelTr;
    
    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority)
            Runner.Despawn(Object);
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            
            Utils.SetRenderLayerInChildren(playerModelTr,LayerMask.NameToLayer("LocalPlayerModel"));
            
            //Disable main camera
            Camera.main.gameObject.SetActive(false);
            
            Debug.Log("Spawned local player");
        }

        else
        {
            // //Disable the camera if we are not the player
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;
            
            // //Only 1 audio listener is allowed in the scene so disable remote players audio listener
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;
        }
    }
}
