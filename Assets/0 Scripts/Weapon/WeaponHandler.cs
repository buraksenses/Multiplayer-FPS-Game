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

    static void OnFireChanged(Changed<WeaponHandler> changed)
    {
        Debug.Log($"{changed.Behaviour.Runner.SimulationTime} OnFireChanged value {changed.Behaviour.isFiring}");

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
        if(Runner.SimulationTime - lastTimeFired < .15f)
            return;

        StartCoroutine(FireEffectRoutine());
        lastTimeFired = Runner.SimulationTime;
    }

    IEnumerator FireEffectRoutine()
    {
        isFiring = true;
        fireParticleSystem.Play();
        
        yield return new WaitForSeconds(.09f);
        isFiring = false;
    }
}
