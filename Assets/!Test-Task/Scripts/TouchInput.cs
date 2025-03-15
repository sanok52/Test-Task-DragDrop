using System;
using UnityEngine;

/// <summary>
/// Управление через сенсорный экран
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

            if (Input.touchCount > 1) //Если несколько нажатий...
            {
                Touch touchSecond = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Moved || touchSecond.phase == TouchPhase.Moved) //То пытаемся реализовать "зум", считая изменения движения обоих пальцев
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
                    case TouchPhase.Moved: //Важно: Передаём дельту относительно мира! Это поможет при таскании объектов в мире
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
