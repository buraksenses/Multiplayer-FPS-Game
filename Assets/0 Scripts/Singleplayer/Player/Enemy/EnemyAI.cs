using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GraduationProject.Managers;
using GraduationProject.SinglePlayer.Managers;
using GraduationProject.SinglePlayer.Player;
using UnityEngine;

namespace GraduationProject.SinglePlayer.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        private Transform _targetTransform;
        private int _healthValue = 100;
        
        private EnemySpawnManager _enemySpawnManager;
        
        private void OnEnable()
        {
            InitializeEnemy();
        }

        public void DecreaseHealth(int damage)
        {
            _healthValue -= damage;
        }

        private void MoveTowardsPlayer()
        {
            var transform1 = transform;
            transform1.position = Vector3.MoveTowards(transform1.position, _targetTransform.position, Time.deltaTime * 2);
        }

        private void Die()
        {
            if (_healthValue > 0) return;
            _enemySpawnManager.DespawnEnemies(transform);
            EventManager.onUpdate -= MoveTowardsPlayer;
            GetComponent<RagdollToggle>().RagdollActivate(true);
            EventManager.onUpdate -= Die;
        }

        private void InitializeEnemy()
        {
            _enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
            _targetTransform = FindObjectOfType<PlayerMovement>().transform.GetChild(2);

            transform.LookAt(_targetTransform);
            _healthValue = 100;

            // ===== EVENT ASSIGNMENTS =====
            EventManager.onUpdate += MoveTowardsPlayer;
            EventManager.onUpdate += Die;
        }

        private void OnCollisionExit(Collision other)
        {
            if (!other.collider.CompareTag("Plane")) return;
            
            DOVirtual.DelayedCall(2f, () =>
            {
                _healthValue = 0;
                Die();
            });
        }
    }
}

