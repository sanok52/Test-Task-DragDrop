using UnityEngine;
using System;

/// <summary>
/// Файл для хранения глобальных данных (сам класс пока не понадобился)
/// </summary>
public class GData : MonoBehaviour
{ }

//Интерфейс, реализующий управление
public interface IInput
{
    public event Action<Vector2> OnSwipe; //Движение по двум осям
    public event Action<Vector2> OnPointDown;
    public event Action<Vector2> OnPointUp;
    public event Action<float> OnScroll; //Приближение/отдаление (не пригодилось пока)
}