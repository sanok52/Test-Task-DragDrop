using UnityEngine;
using System;

/// <summary>
/// ���� ��� �������� ���������� ������ (��� ����� ���� �� �����������)
/// </summary>
public class GData : MonoBehaviour
{ }

//���������, ����������� ����������
public interface IInput
{
    public event Action<Vector2> OnSwipe; //�������� �� ���� ����
    public event Action<Vector2> OnPointDown;
    public event Action<Vector2> OnPointUp;
    public event Action<float> OnScroll; //�����������/��������� (�� ����������� ����)
}