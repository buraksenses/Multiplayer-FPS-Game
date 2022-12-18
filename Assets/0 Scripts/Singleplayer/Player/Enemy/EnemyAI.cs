using System;
using System.Collections;
using System.Collections.Generic;
using GraduationProject.SinglePlayer.Player;
using UnityEngine;

namespace GraduationProject.SinglePlayer.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        private Transform _targetTransform;
        
        private void OnEnable()
        {
            _targetTransform = FindObjectOfType<PlayerMovement>().transform.GetChild(2);
            transform.LookAt(_targetTransform);
        }

        private void Update()
        {
            var transform1 = transform;
            transform1.position = Vector3.MoveTowards(transform1.position, _targetTransform.position, Time.deltaTime * 2);
        }
    }
}

