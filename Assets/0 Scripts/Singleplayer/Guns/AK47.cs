using System;
using System.Collections;
using System.Collections.Generic;
using GraduationProject.Managers;
using GraduationProject.SinglePlayer.Managers;
using UnityEngine;

namespace GraduationProject.Guns
{
    public class AK47 : Gun
    {
        private Animator _animator;
        private Transform _fpsCamTr;
        private int _layerMask = 1 << 8;
        
        private void Awake()
        {
            headShotDamageValue = 108;
            ammoCapacity = 30;
            recoilTime = .2f;
            
            _animator = GetComponentInParent<Animator>();
        }

        private void Start()
        {
            _fpsCamTr = Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            // ===== EVENT ASSIGNMENTS =====
            EventManager.onUpdate += Fire;
        }
    
        private void Fire()
        {
            recoilTime += Time.deltaTime;
            
            if (!Input.GetKey(KeyCode.Mouse0) || recoilTime <= .2f) return;
            PlayShootAnimation();
            PlayMuzzleFlashEffect();
            CreateProjectileRay();
            recoilTime = 0;
        }
    
        private void PlayShootAnimation()
        {
            _animator.SetTrigger("Shoot");
        }
    
        private void PlayMuzzleFlashEffect()
        {
            EffectManager.Instance.PlayMuzzleFlashEffect(effectSpawnPoint.position);
        }
    
        private void CreateProjectileRay()
        {
            if (!Input.GetKey(KeyCode.Mouse0) || recoilTime <= .2f) return;
    
            RaycastHit hit;
            if(Physics.Raycast(_fpsCamTr.transform.position, _fpsCamTr.transform.forward, out hit, 100,_layerMask))
            {
                Debug.DrawRay(_fpsCamTr.transform.position,_fpsCamTr.transform.forward * 100,Color.red,2f);
                print($"{bodyDamageValue} damage given");
            }
        }
        
    }
}

