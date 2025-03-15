using System;
using UnityEngine;

/// <summary>
/// ���������� ����� ��������� �����
/// </summary>
public class TouchInput : MonoBehaviour, IInput
{
    public event Action<Vector2> OnSwipe;
    public event Action<Vector2> OnPointDown;
    public event Action<Vector2> OnPointUp;
    public event Action<float> OnScroll;

    private bool isHold;

    public bool IsHold { get => isHold; }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (Input.touchCount > 1) //���� ��������� �������...
            {
                Touch touchSecond = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Moved || touchSecond.phase == TouchPhase.Moved) //�� �������� ����������� "���", ������ ��������� �������� ����� �������
                {
                    OnScroll?.Invoke(touch.deltaPosition.magnitude + touchSecond.deltaPosition.magnitude);
                }
            }
            else
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isHold = true;
                        OnPointDown?.Invoke(touch.position);
                        break;
                    case TouchPhase.Moved: //�����: ������� ������ ������������ ����! ��� ������� ��� �������� �������� � ����
                        Vector3 worldDelta = Camera.main.ScreenToWorldPoint(new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0)) -
                                    Camera.main.ScreenToWorldPoint(Vector3.zero);
                        OnSwipe?.Invoke(worldDelta);
                        break;
                    case TouchPhase.Ended:
                        isHold = false;
                        OnPointUp?.Invoke(touch.position);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
