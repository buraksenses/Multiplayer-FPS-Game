using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GraduationProject.Managers;
using UnityEngine;

namespace GraduationProject.SinglePlayer.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private float cameraRotaionX = 0f;
        private float cameraRotaionY = 0f;

        [Header("Component References")] 
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private Camera camera;

        private bool _isGrounded;

        private void Start()
        {
            EventManager.onUpdate += Move;
            EventManager.onUpdate += Jump;
            EventManager.onUpdate += Look;
        }

        private void Move()
        {
            var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed;
            var transform1 = transform;
            var moveDirection = transform1.forward * dir.z + transform1.right * dir.x;
            transform1.localPosition += moveDirection;
        }

        private void Jump()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;
            transform.position+=Vector3.up;
        }

        private void Look()
        {
            var viewInputX = Input.GetAxis("Mouse X") * 20;
            var viewInputY = Input.GetAxis("Mouse Y") * 20;
            
            cameraRotaionX += -(viewInputY * Time.deltaTime *
                                20);
            cameraRotaionX = Mathf.Clamp(cameraRotaionX, -90, 90);

            cameraRotaionY += viewInputX * Time.deltaTime * 20;
        
            transform.localRotation = Quaternion.Euler(cameraRotaionX,cameraRotaionY,0);

        }
    }
}

