using System;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T: PoolableObject
{
    [SerializeField] protected ObjectPool<T> ObjectPool;

    public event Action CountChanged;

    public int SpawnedObjectsCount { get; private set; }
    public int CreatedObjectsCount { get; private set; }
    public int ActiveObjectsCount { get; private set; }

    private void OnEnable()
    {
        ObjectPool.CreatedObjectCountChanged += OnCreatedObjectsCountChanged;
    }

    private void OnDisable()
    {
        ObjectPool.CreatedObjectCountChanged -= OnCreatedObjectsCountChanged;
    }

    public void Spawn(Vector3 position)
    {
        SpawnedObjectsCount++;
        ActiveObjectsCount++;
        CountChanged?.Invoke();

        PoolableObject poolableObject = ObjectPool.Get(position);
        poolableObject.Disabled += OnObjectDisable;
    }

    protected virtual void OnObjectDisable(PoolableObject poolableObject)
    {
        ActiveObjectsCount--;
        CountChanged?.Invoke();

        poolableObject.Disabled -= OnObjectDisable;
        ObjectPool.Release((T)poolableObject);
    }

    private void OnCreatedObjectsCountChanged()
    {
        CreatedObjectsCount = ObjectPool.CreatedObjectsCount;

        CountChanged?.Invoke();
    }
}
