using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraduationProject.Guns
{
    public abstract class Gun : MonoBehaviour
    {
        public int bodyDamageValue;
        protected int headShotDamageValue;
        protected int ammoCapacity;
    }
}

