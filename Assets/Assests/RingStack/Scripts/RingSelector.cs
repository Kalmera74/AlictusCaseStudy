using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RingStack.Scripts
{


    public class RingSelector : MonoBehaviour
    {

        [SerializeField] RayCastSelector RayCastSelector;

        private void Awake()
        {
            if (RayCastSelector == null)
            {
                RayCastSelector = GetComponent<RayCastSelector>();
            }
        }
        public GameObject TryAndGetRing()
        {
            var ring = RayCastSelector.CastRayFromCameraToMouseInWorld();

            return ring;
        }
    }


}