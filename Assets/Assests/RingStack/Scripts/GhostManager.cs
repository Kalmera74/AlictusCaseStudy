using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GhostManager : MonoBehaviour
{
    [SerializeField] private RingType RingType;

    public bool CompareRingType(RingType ringType)
    {
        return RingType.Equals(ringType);
    }
}
