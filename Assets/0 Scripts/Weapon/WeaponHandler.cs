using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class WeaponHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnFireChanged))]
    public bool isFiring { get; set; }

    public ParticleSystem fireParticleSystem;
    
    private float lastTimeFired = 0;

    public Transform aimPoint;

    public LayerMask collisionLayers;

    static void OnFireChanged(Changed<WeaponHandler> changed)
    {
        //Debug.Log($"{changed.Behaviour.Runner.SimulationTime} OnFireChanged value {changed.Behaviour.isFiring}");

        bool isFiringCurrent = changed.Behaviour.isFiring;
        
        //Load the old value
        changed.LoadOld();

        bool isFiringOld = changed.Behaviour.isFiring;
        
        if(isFiringCurrent && !isFiringOld)
            changed.Behaviour.OnFireRemote();
    }

    void OnFireRemote()
    {
        if(!Object.HasInputAuthority)
            fireParticleSystem.Play();
    }

    public void Fire(Vector3 aimForwardVector)
    {
        if(Time.time - lastTimeFired < .15f)
            return;

        StartCoroutine(FireEffectRoutine());

        Runner.LagCompensation.Raycast(aimPoint.position, aimForwardVector, 100, Object.InputAuthority, out var hitInfo,
            collisionLayers,HitOptions.IncludePhysX);

        float hitDistance = 100;
        bool isHitOtherPlayer = false;

        if (hitInfo.Hitbox != null)//If we hit a fusion collider
        {
            Debug.Log($"{Time.time} {transform.name} hit hitbox of {hitInfo.Hitbox.transform.root.name}");

            if(Object.HasStateAuthority)
                hitInfo.Hitbox.transform.root.GetComponent<HPHandler>().OnTakeDamage();
            
            isHitOtherPlayer = true;
        }
        else if (hitInfo.Collider != null)//If we hit a PhysX collider
        {
            Debug.Log($"{Time.time} {transform.name} hit PhysX collider of {hitInfo.Collider.transform.name}");
        }

        if (hitInfo.Distance > 0)
            hitDistance = hitInfo.Distance;
        
        if(isHitOtherPlayer)
            Debug.DrawRay(aimPoint.position,aimForwardVector*hitDistance,Color.red,1);
        else Debug.DrawRay(aimPoint.position,aimForwardVector*hitDistance,Color.green,1);
        
        lastTimeFired = Time.time;
    }

    IEnumerator FireEffectRoutine()
    {
        isFiring = true;
        fireParticleSystem.Play();
        
        yield return new WaitForSeconds(.09f);
        isFiring = false;
    }
}
