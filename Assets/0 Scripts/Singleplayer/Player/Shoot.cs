using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Fusion;
using GraduationProject.Guns;
using GraduationProject.Managers;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace GraduationProject.Guns
{
    public class Shoot : MonoBehaviour
    {
        private Animator _animator;
        private Transform _fpsCamTr;
        private float _recoilTime = .2f;
        private int _layerMask = 1 << 8;
    
        private Gun _gun;
        
        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
            _gun = GetComponent<Gun>();
        }
    
        private void Start()
        {
            _fpsCamTr = Camera.main.transform;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            // ===== EVENT ASSIGNMENTS =====
            EventManager.onUpdate += Fire;
            EventManager.onUpdate += CharacterRotation;
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
            if(Physics.Raycast(_fpsCamTr.transform.position, _fpsCamTr.transform.forward, out hit, 100,_layerMask))
            {
                Debug.DrawRay(_fpsCamTr.transform.position,_fpsCamTr.transform.forward * 100,Color.red,2f);
                print($"{_gun.bodyDamageValue} damage given");
            }
        }

        private void CharacterRotation()
        {
            transform.root.rotation = _fpsCamTr.rotation;
        }
        
    }
}


