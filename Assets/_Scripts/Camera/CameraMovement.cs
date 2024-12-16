using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _transformLookAt;
    
    private Vector3 _initPos;
    private Quaternion _initRot;

    private void Start()
    {
        _initPos = transform.position;
        _initRot = transform.rotation;
    }

    public void ZoomCamera()
    {
        transform.LookAt(_transformLookAt, Vector3.up);
    }

    public void ResetCamera()
    {
        transform.LookAt(Vector3.left, Vector3.up);
    }

}
