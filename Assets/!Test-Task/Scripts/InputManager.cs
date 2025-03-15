using System;
using UnityEngine;

/// <summary>
/// Менеджер, который решает, как регировать на нажатие и движение
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] private CameraMove _cameraMove;
    [SerializeField] private DragSystem _dragSystem;

    private IInput _input;

    /// <summary>
    /// Инициализация управления
    /// </summary>
    /// <param name="input">Тип управления</param>
    public void Init(IInput input)
    {
        _input = input;
        _input.OnSwipe += OnSwipe;
        _input.OnPointUp += OnPointUp;
        _input.OnPointDown += OnPointDown;
    }

    private void OnSwipe (Vector2 directionMove)
    {
        if (_dragSystem.CurrentDragable != null) //Если ничего не тащим, то двигаем камеру
        {
            Drag(directionMove);
        }
        else
        {
            MoveCamera(directionMove);
        }
    }

    private void MoveCamera(Vector2 directionMove)
    {
        _cameraMove.MoveCamera(directionMove * Time.deltaTime);
    }

    private void Drag(Vector2 directionMove)
    {
        _dragSystem.MoveDragable(directionMove);
    }

    private void OnPointDown (Vector2 position)
    {
        _dragSystem.PointToDrag(position);
    }

    private void OnPointUp(Vector2 position)
    {
        _dragSystem.DropDragable();
    }
}