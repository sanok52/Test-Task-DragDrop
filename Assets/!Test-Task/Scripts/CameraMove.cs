using System.Collections;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Space]
    [SerializeField] private float _speedMove = 5f;

    [Space]
    [SerializeField] private Vector2 _minMoveLimit;
    [SerializeField] private Vector2 _maxMoveLimit;

    [Space]
    [SerializeField] private bool _invertX = true;
    [SerializeField] private bool _invertY = false;

    public void MoveCamera(Vector2 directon)
    {
        Vector3 xV3 = ((_invertX ? -1f : 1f) * Vector3.right * directon.x);
        Vector3 yV3 = ((_invertY ? -1f : 1f) * Vector3.up * directon.y);
        Vector3 npos = _camera.transform.position + ((xV3 + yV3) * _speedMove);
        _camera.transform.position = new Vector3(Mathf.Clamp(npos.x, _minMoveLimit.x, _maxMoveLimit.x),
            Mathf.Clamp(npos.y, _minMoveLimit.y, _maxMoveLimit.y),
            npos.z);
    }
}