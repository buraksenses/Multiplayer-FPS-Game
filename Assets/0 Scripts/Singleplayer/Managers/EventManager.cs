using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraduationProject.Managers
{
    public class EventManager : MonoBehaviour
    {
        public static event Action onUpdate,onFixedUpdate; 
        
        public static void ResetEvents()
        {
            onUpdate = null;
        }

        private void Update()
        {
            onUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            onFixedUpdate?.Invoke();
        }
    }
}

