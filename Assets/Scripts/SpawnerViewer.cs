using TMPro;
using UnityEngine;

public class SpawnerViewer<T> : MonoBehaviour where T: PoolableObject
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _spawnedObjectsCountUI;
    [SerializeField] private TextMeshProUGUI _createdObjectsCountUI;
    [SerializeField] private TextMeshProUGUI _activeObjectsCountUI;

    private void Awake()
    {
        ShowValues();
    }

    private void ShowValues()
    {
        _spawnedObjectsCountUI.text = $"Количество заспавленных объектов {_spawner.SpawnedObjectsCount}";
        _createdObjectsCountUI.text = $"Количество сзданных объектов {_spawner.CreatedObjectsCount}";
        _activeObjectsCountUI.text = $"Количество активных объектов {_spawner.ActiveObjectsCount}";
    }

    private void OnEnable()
    {
        _spawner.CountChanged += ShowValues;
    }

    private void OnDisable()
    {
        _spawner.CountChanged -= ShowValues;
    }
}
