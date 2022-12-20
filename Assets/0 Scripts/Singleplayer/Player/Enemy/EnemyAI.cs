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
        [Header("Private Properties")]
        private int _healthValue = 100;
        
        [Header("Component References")]
        private Transform _targetTransform;
        private AudioSource _audioSource;
        
        [Header("Script References")]
        private EnemySpawnManager _enemySpawnManager;

        private void OnEnable()
        {
            InitializeEnemy();
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void InitializeEnemy()
        {
            transform.LookAt(_targetTransform);
            _healthValue = 100;
            
            _enemySpawnManager = FindObjectOfType<EnemySpawnManager>();
            _targetTransform = FindObjectOfType<PlayerMovement>().transform.GetChild(2);

            // ===== EVENT ASSIGNMENTS =====
            EventManager.onUpdate += MoveTowardsPlayer;
            EventManager.onUpdate += Die;
            EventManager.onUpdate += CheckFallRespawn;
        }

        public void DecreaseHealth(int damage)
        {
            _healthValue -= damage;
        }

        private void MoveTowardsPlayer()
        {
            var transform1 = transform;
            transform1.LookAt(_targetTransform);
            transform1.position = Vector3.MoveTowards(transform1.position, _targetTransform.position, Time.deltaTime * 2);
        }

        private void Die()
        {
            if (_healthValue > 0) return;
            
            _enemySpawnManager.DespawnEnemies(transform);           
            GetComponent<RagdollToggle>().RagdollActivate(true);
            
            EventManager.onUpdate -= MoveTowardsPlayer;
            EventManager.onUpdate -= Die;
            EventManager.onUpdate -= CheckFallRespawn;
            _audioSource.Stop();
        }

        private void CheckFallRespawn()
        {
            if (!(transform.position.y < -6)) return;
            _healthValue = 0;
            Die();
        }

        private void PlayFootStepSound()
        {
            _audioSource.Play();
        }
    }
}

