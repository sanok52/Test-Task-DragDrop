using UnityEngine;

/// <summary>
/// Инициализация уровня
/// </summary>
public class GameLevelInit : MonoBehaviour
{
    [SerializeField] private InputManager _cameraManager;
    [SerializeField] private GameObject _input; //Объект с IInput для реализации управления

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cameraManager.Init(_input.GetComponent<IInput>()); //Передаём в cameraManager тип управления, выбранный нами
    }
}
