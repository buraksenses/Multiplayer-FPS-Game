using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using GraduationProject.Managers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Animator _animator;
    private Camera _fpsCam;
    private float _recoilTime = .2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _fpsCam = Camera.main;
        
        // ===== EVENT ASSIGNMENTS =====
        EventManager.onUpdate += Fire;
    }

    private void Fire()
    {
        _recoilTime += Time.deltaTime;
        
        if (!Input.GetKey(KeyCode.Mouse0) || _recoilTime <= .2f) return;
        PlayShootAnimation();
        PlayMuzzleFlashEffect();
        CreateProjectileRay();
        _recoilTime = 0;
    }

    private void PlayShootAnimation()
    {
        _animator.SetTrigger("Shoot");
    }

    private void PlayMuzzleFlashEffect()
    {
        
    }

    private void CreateProjectileRay()
    {
        if (!Input.GetKey(KeyCode.Mouse0) || _recoilTime <= .2f) return;

        RaycastHit hit;
        if(Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out hit, 100))
        {
            Debug.DrawRay(_fpsCam.transform.position,_fpsCam.transform.forward * 100,Color.red,2f);
        }
    }
    
}
