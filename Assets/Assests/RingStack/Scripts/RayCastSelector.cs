using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastSelector : MonoBehaviour
{

    [SerializeField] private LayerMask RayCastMask;
    [SerializeField] private Camera MainCamera;
    private void Awake()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    public GameObject CastRayFromCameraToMouseInWorld()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.direction * 1000, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, RayCastMask))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}