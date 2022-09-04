using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RingStack.Scripts
{


    public class Person : MonoBehaviour
    {
        [SerializeField] Animator AnimatorController;
        [SerializeField] List<Ring> Stack = new List<Ring>();

        public void Add(Ring ring)
        {
            Stack.Add(ring);
        }
        public void Remove(Ring ring)
        {
            Stack.Remove(ring);
        }

        public bool GetAllRingsAreSame()
        {
            Ring prevRing = null;
            if (Stack.Count == 1)
            {
                return false;
            }
            foreach (var ring in Stack)
            {
                if (prevRing == null)
                {
                    prevRing = ring;
                }
                else
                {
                    if (!prevRing.CompareRingType(ring.GetRingType()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool GetIsWinner()
        {
            return Stack.Count == 0;
        }

        private void OnTriggerEnter(Collider other)
        {

            var ring = other.gameObject.GetComponent<Ring>();

            if (ring)
            {
                Add(ring);
            }
        }

        private void OnTriggerExit(Collider other)
        {

            var ring = other.gameObject.GetComponent<Ring>();

            if (ring)
            {
                Remove(ring);
            }
        }

        internal void PlayWinAnimation()
        {
            AnimatorController.CrossFade("Win", .15f);
        }
    }
}