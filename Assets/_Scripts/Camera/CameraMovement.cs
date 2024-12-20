using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _transformLookAt;
    [SerializeField] private float zoomPos;
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
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(_initPos.x, _initPos.y, _initPos.z + zoomPos);
        StartCoroutine(GoToTarget(startPos, targetPos));
    }

    private IEnumerator GoToTarget(Vector3 startPos, Vector3 targetPos)
    {
        float duration = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            yield return null;
        }

        transform.position = targetPos;
    }

    public void ResetCamera()
    {
        transform.LookAt(Vector3.left, Vector3.up);
        transform.rotation =_initRot;
        StartCoroutine(GoToTarget(transform.position, _initPos));
    }

}
