using UnityEngine;

/// <summary>
/// ������������� ������
/// </summary>
public class GameLevelInit : MonoBehaviour
{
    [SerializeField] private InputManager _cameraManager;
    [SerializeField] private GameObject _input; //������ � IInput ��� ���������� ����������

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cameraManager.Init(_input.GetComponent<IInput>()); //������� � cameraManager ��� ����������, ��������� ����
    }
}
