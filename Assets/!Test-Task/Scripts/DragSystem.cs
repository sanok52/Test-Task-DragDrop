using UnityEngine;
using System;

/// <summary>
/// Система таскания предметов
/// </summary>
public class DragSystem : MonoBehaviour
{
    [SerializeField] private LayerMask _layersDrag;
    private Dragable _currentDragable;

    public event Action<Dragable> OnDrag;
    public event Action<Dragable> OnDrop;
    public event Action<Dragable, Vector3> OnMove;

    public Dragable CurrentDragable { get => _currentDragable; }

    /// <summary>
    /// Кликаем по области, чтобы зацепить объект
    /// </summary>
    /// <param name="position"></param>
    public bool PointToDrag (Vector2 position)
    {
        if (_currentDragable != null)
            return false;

        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D hitInfo = Physics2D.Raycast(touchPosition, Vector2.zero, Mathf.Infinity, _layersDrag);
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.TryGetComponent(out Dragable dragable))
            {
                SetDragable(dragable);
                return true;
            }
        }
        return false;
    }

    private void SetDragable(Dragable dragable)
    {
        _currentDragable = dragable;
        _currentDragable.StartDrag();
        OnDrag?.Invoke(dragable);
    }

    public void DropDragable()
    {
        DropDragable(_currentDragable);
    }

    private void DropDragable(Dragable dragable)
    {
        if (dragable == null)
            return;

        _currentDragable.EndDrag();
        _currentDragable = null;
        OnDrop?.Invoke(dragable);
    }

    public void MoveDragable(Vector2 directionMove)
    {
        if (_currentDragable == null)
            return;

        _currentDragable.transform.position += (Vector3)directionMove;
    }
}