using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T: PoolableObject
{
    [SerializeField] private int _capacity;
    [SerializeField] private T _prefab;

    private Queue<T> _pool = new();

    public event Action CreatedObjectCountChanged;

    public int CreatedObjectsCount { get; private set; }

    private void Awake()
    {
        Fill(_prefab);
    }

    private void Fill(T prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            T newObject = CreateObject(prefab);
            newObject.gameObject.SetActive(false);
            _pool.Enqueue(newObject);
        }
    }

    public T Get(Vector3 positon)
    {
        T newPoolableObject;

        if (_pool.Count > 0)
        {
            newPoolableObject = _pool.Dequeue();
        }
        else
        {
            newPoolableObject = CreateObject(_prefab);
        }

        newPoolableObject.transform.position = positon;
        newPoolableObject.gameObject.SetActive(true);

        return newPoolableObject;
    }

    private T CreateObject(T prefab) 
    {
        CreatedObjectsCount++;
        CreatedObjectCountChanged?.Invoke();

        return Instantiate(prefab);
    }

    public void Release(T poolableObject)
    {
        _pool.Enqueue(poolableObject);
    }
}
