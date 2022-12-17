using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EZ_Pooling;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace GraduationProject.SinglePlayer.Managers
{
    public class EffectManager : Singleton<EffectManager>
    {
        [SerializeField] private Transform muzzleFlashEffect;
        [SerializeField] private Transform smokeEffect;

        public void PlayMuzzleFlashEffect(Vector3 pos)
        {
            var muzzleFlashTr = EZ_PoolManager.Spawn(muzzleFlashEffect, pos, Quaternion.identity);
            var smokeTr = EZ_PoolManager.Spawn(smokeEffect, pos, Quaternion.identity);
            DOVirtual.DelayedCall(.1f, () =>
            {
                EZ_PoolManager.Despawn(muzzleFlashTr);
            });

            DOVirtual.DelayedCall(.3f, () =>
            {
                EZ_PoolManager.Despawn(smokeTr);
            });
        }
        
    }
}


