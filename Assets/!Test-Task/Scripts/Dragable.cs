using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Таскаемый объект
/// </summary>
public class Dragable : MonoBehaviour
{
    [SerializeField] private DropDown _dropDown;
    private bool isDrag;

    public bool IsDrag { get => isDrag; }

    private void Start()
    {
        if(_dropDown == null)
            _dropDown = GetComponent<DropDown>();
    }

    public void StartDrag()
    {
        _dropDown?.SetUseGravity(false); //Отключаем гравитацию
        isDrag = true;
    }

    public void EndDrag()
    {
        _dropDown?.SetUseGravity(true); //Включаем гравитацию
        isDrag = false;
    }
}