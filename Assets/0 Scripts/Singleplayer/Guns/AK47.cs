using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraduationProject.Guns
{
    public class AK47 : Gun
    {
        private void Awake()
        {
            bodyDamageValue = 35;
            headShotDamageValue = 108;
            ammoCapacity = 30;
        }
        
    }
}

