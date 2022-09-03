using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RingType
{
    Red,
    Green
}
public class Ring : MonoBehaviour
{
    [SerializeField] private LayerMask PersonLayerMask;
    [SerializeField] private LayerMask RingLayerMask;
    [SerializeField] private RingType RingType;
    private Vector3 _personDetectionRayOrigin;
    private Vector3 _personDetectionRayDestination;
    private Vector3 _ringDetectionRayOrigin;
    private Vector3 _ringDetectionRayDestination;

    private bool _canMoveVertical;
    private bool _canMoveHorizontal;
    private bool _canPlaceAtTheSpot = false;
    private float _personDetectionRayDistance = 2f;



    private void Update()
    {
        CalculateRayDirections();
    }
    private void CalculateRayDirections()
    {
        _personDetectionRayOrigin = transform.position + Vector3.left * .2f;

        _personDetectionRayDestination = transform.position - _personDetectionRayOrigin;


        _ringDetectionRayOrigin = transform.position;
        _ringDetectionRayDestination = transform.TransformDirection(Vector3.up);

    }

    public Vector3 GetPositionToDrop()
    {
        RaycastHit hit;
        if (Physics.Raycast(_ringDetectionRayOrigin, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, RingLayerMask))
        {

            if (hit.collider.gameObject.CompareTag("Ring"))
            {
                var ring = hit.collider.gameObject.GetComponent<Ring>();
                if (ring)
                {
                    if (ring.CompareRingType(RingType))
                    {
                        _canPlaceAtTheSpot = true;
                        var result = hit.collider.gameObject.transform.localPosition;
                        result.y += .2f;
                        return result;
                    }
                }
            }
            else if (hit.collider.gameObject.CompareTag("StackPoint"))
            {
                _canPlaceAtTheSpot = false;
                var result = transform.localPosition;
                result.y = 0f;
                return result;
            }
        }
        _canPlaceAtTheSpot = false;
        return Vector3.zero;

    }
    public RingType GetRingType()
    {
        return RingType;
    }
    public bool GetCanPlaceAtTheSpot()
    {
        return _canPlaceAtTheSpot;
    }
    public bool CompareRingType(RingType ringType)
    {
        return RingType.Equals(ringType);
    }
    public void CheckCollisions()
    {
        _canMoveVertical = !Physics.Raycast(_ringDetectionRayOrigin, _ringDetectionRayDestination, .2f, RingLayerMask);
        _canMoveHorizontal = !Physics.Raycast(_personDetectionRayOrigin, _personDetectionRayDestination, _personDetectionRayDistance, PersonLayerMask);
    }

    public bool GetCanMoveVertical()
    {
        CheckCollisions();
        return _canMoveVertical;
    }
    public bool GetCanMoveHorizontal()
    {
        CheckCollisions();
        return _canMoveHorizontal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_personDetectionRayOrigin, _personDetectionRayDestination * _personDetectionRayDistance);


        Gizmos.color = Color.black;
        Gizmos.DrawRay(_ringDetectionRayOrigin, _ringDetectionRayDestination * .2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_ringDetectionRayOrigin, transform.TransformDirection(Vector3.down));
    }

  
}
