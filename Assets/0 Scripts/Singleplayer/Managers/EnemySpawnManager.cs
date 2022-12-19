using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EZ_Pooling;
using GraduationProject.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GraduationProject.SinglePlayer.Managers
{
    public class EnemySpawnManager : MonoBehaviour
    {
        public List<Transform> enemies;

        [SerializeField] private Transform enemyPrefab;
        
        private int _minusEffectorX; //Randomize the spawn point's X axis value
        private int _minusEffectorZ; //Randomize the spawn point's Z axis value
        
        private short _maxEnemyAtATime = 10; //Maximum number of enemies in the scene
        private short _minSpawnXandZ = 10; //Minimum x and z values of spawn point
        private short _maxSpawnXandZ = 40; // Maximum x and z points of spawn point

        private void Start()
        {
            // ===== EVENT ASSIGNMENTS =====
            EventManager.onUpdate += SpawnEnemies;
        }

        private void SpawnEnemies()
        {
            DOVirtual.DelayedCall(2f, () =>
            {
                if (enemies.Count >= _maxEnemyAtATime) return;

                _minusEffectorX = Random.Range(0, 100) >= 50 ? 1 : -1;
                _minusEffectorZ = Random.Range(0, 100) >= 50 ? 1 : -1;
            
                var enemy = EZ_PoolManager.Spawn(enemyPrefab,
                    new Vector3(Random.Range(_minusEffectorX * _minSpawnXandZ, _minusEffectorX * _maxSpawnXandZ), 2,
                        Random.Range(_minusEffectorZ * _minSpawnXandZ, _minusEffectorZ * _maxSpawnXandZ)),Quaternion.identity);
                enemies.Add(enemy);
            });
        }

        public void DespawnEnemies(Transform enemy)
        {
            if (enemies.Count == 0) return;
            if(!enemies.Contains(enemy))return;
            DOVirtual.DelayedCall(2f, () =>
            {
                EZ_PoolManager.Despawn(enemy);
                enemies.Remove(enemy);
            });
        }
    }
}

