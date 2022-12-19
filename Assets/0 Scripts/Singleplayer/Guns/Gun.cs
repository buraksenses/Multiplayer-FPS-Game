using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraduationProject.SinglePlayer.Guns
{
    public abstract class Gun : MonoBehaviour
    {
        public Transform effectSpawnPoint;
        
        public int bodyDamageValue;
        public int headShotDamageValue;
        public int ammoCapacity;
        public float recoilTime;

        public AudioSource gunAudioSource;
        public AudioClip fireSound;

        protected abstract void Fire();
    }
}

