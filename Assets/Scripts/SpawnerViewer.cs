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
        _spawnedObjectsCountUI.text = $"���������� ������������ �������� {_spawner.SpawnedObjectsCount}";
        _createdObjectsCountUI.text = $"���������� �������� �������� {_spawner.CreatedObjectsCount}";
        _activeObjectsCountUI.text = $"���������� �������� �������� {_spawner.ActiveObjectsCount}";
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
