using UnityEngine;

/// <summary>
/// Отдаление объектов по Z из-за их положения по Y
/// </summary>
public class SortingAtY : MonoBehaviour
{
    [SerializeField] private float _coefficient = 0.01f;
    [SerializeField] private bool _autoUpdate = true;

    void Update()
    {
        if(_autoUpdate)
            Sorting(transform.position.y);
    }

    public void Sorting (float y)
    {
        transform.position =new Vector3(transform.position.x, transform.position.y, y * _coefficient);
    }
}
