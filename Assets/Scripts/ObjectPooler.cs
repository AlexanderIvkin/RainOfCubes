using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> : MonoBehaviour where T: PoolableObject
{
    [SerializeField] private int _capacity;

    [SerializeField] private T _prefab;

    private Queue<T> _objectsInPool = new();

    public int CreatedObjectsCount => _objectsInPool.Count;

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
            _objectsInPool.Enqueue(newObject);
        }
    }

    public T Get(Vector3 positon)
    {
        T newPoolableObject;

        if (_objectsInPool.Count > 0)
        {
            newPoolableObject = _objectsInPool.Dequeue();
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
        return Instantiate(prefab);
    }

    public void Release(T poolableObject)
    {
        _objectsInPool.Enqueue(poolableObject);
    }
}
