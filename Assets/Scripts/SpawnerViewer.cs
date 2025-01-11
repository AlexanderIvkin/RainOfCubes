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
        _spawnedObjectsCountUI.text = $"Количество заспавленных объектов {_spawner.SpawnedObjectsCount}";
        _createdObjectsCountUI.text = $"Количество сзданных объектов {_spawner.CreatedObjectsCount}";
        _activeObjectsCountUI.text = $"Количество заспавленных объектов {_spawner.ActiveObjectsCount}";
    }
}
