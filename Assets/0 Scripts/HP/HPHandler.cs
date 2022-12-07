using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class HPHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnHPChanged))]
    private byte HP { get; set; }
    
    [Networked(OnChanged = nameof(OnStateChanged))]
    public bool isDead { get; set; }

    private bool isInitialized = false;
    private const byte startingHP = 5;

    private void Start()
    {
        HP = startingHP;
        isDead = false;
    }

    public void OnTakeDamage()
    {
        if(isDead)
            return;
        
        HP -= 1;
        
        Debug.Log($"{Time.time} {transform.name} took damage got {HP} left");
        
        //Player died
        if (HP <= 0)
        {
            Debug.Log($"{Time.time} {transform.name} died");

            isDead = true;
        }
    }

    static void OnHPChanged(Changed<HPHandler> changed)
    {
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.HP}");
    }

    static void OnStateChanged(Changed<HPHandler> changed)
    {
        Debug.Log($"{Time.time} OnStateChanged isDead {changed.Behaviour.isDead}");
    }
}
