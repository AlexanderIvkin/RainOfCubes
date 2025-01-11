using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerViewer<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _titleNameObjectUI;
    [SerializeField] private TextMeshProUGUI _spawnedObjectsCountUI;
    [SerializeField] private TextMeshProUGUI _createdObjectsCountUI;
    [SerializeField] private TextMeshProUGUI _activeObjectsCountUI;

    private T poolableObject;

    private void Awake()
    {
        _titleNameObjectUI.text = $"{poolableObject.Name}";
        _spawnedObjectsCountUI.text = $"���������� ������������ �������� {_spawner.SpawnedObjectsCount}";
        _createdObjectsCountUI.text = $"���������� �������� �������� {_spawner.CreatedObjectsCount}";
        _activeObjectsCountUI.text = $"���������� ������������ �������� {_spawner.ActiveObjectsCount}";
    }
}
