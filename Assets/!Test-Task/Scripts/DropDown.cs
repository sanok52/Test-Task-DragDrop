using UnityEngine;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// ����� ����������, ���� �� �� ��� ������
/// </summary>
public class DropDown : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private string[] _tagsWalls; //���� ��� ����. ���� ���� - ������, ���� ����� ����� ���� ���
    [SerializeField] private string[] _tagsGround; //���� ��� ����. �� ������ ������ ����
    private List<GameObject> _walls = new List<GameObject>();
    private List<GameObject> _grounds = new List<GameObject>();
    private bool _useGravity = true; //������ �� ������?
    private bool _isGrounded = false; //����� �� �� ����?

    private void Start()
    {
        if(_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();
        SetGroundState(false);
    }

    private void Update()
    {
        //if (_useGravity && _isGrounded == false) //���� �� ������ ������ � �� �� �����, �� �������� ����������
        //    _rigidbody2D.gravityScale = 1f;        
    }

    public void SetUseGravity (bool useGravity)
    {
        _useGravity = useGravity;
        if(useGravity == false)
            _rigidbody2D.gravityScale = 0f;
        if (_useGravity && _isGrounded == false)
            _rigidbody2D.gravityScale = 1f;
    }

    private void SetGroundState(bool isGrounded)
    {
        //Debug.Log($"{gameObject.name} {isGrounded}");
        _isGrounded = isGrounded;
        if (isGrounded)
        {
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.velocity = Vector2.zero; 
        }
        else
        {
            if (_useGravity)
                _rigidbody2D.gravityScale = 1f;
        }
    }

    /// <summary>
    /// ��� ���?
    /// </summary>
    /// <param name="go">������ ��� ��������</param>
    /// <returns></returns>
    private bool IsGround (GameObject go)
    {
        return _tagsGround.Contains(go.tag);
    }

    /// <summary>
    /// ��� �����?
    /// </summary>
    /// <param name="go">������ ��� ��������</param>
    /// <returns></returns>
    private bool IsWall(GameObject go)
    {
        return _tagsWalls.Contains(go.tag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsGround(collision.gameObject))
        {
            _grounds.Add(collision.gameObject);
            if (_walls.Count == 0) //���� ��� ����, �� �������, ��� �� �� �����
                SetGroundState(true);
        }
        else if (IsWall(collision.gameObject)) //���� ��� �����
        {
            _walls.Add(collision.gameObject); //��������� � ������
            SetGroundState(false); //������ �� �� �� �����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsGround(collision.gameObject))
        {
            _grounds.Remove(collision.gameObject);
            if(_grounds.Count == 0) //���� ������ ��� ����, ������ �� ������ �� �� �����
                SetGroundState(false);
        }

        if (IsWall(collision.gameObject))
        {
            _walls.Remove(collision.gameObject);
            if (_walls.Count == 0 && _grounds.Count > 0) //���� ��� ����, �� ���� ���, �� �� �� ����
                SetGroundState(true);
        }
    }
}
