using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler<T> : MonoBehaviour where T: PoolableObject
{
    [SerializeField] private int _capacity;

    private Queue<T> _objectsInPool = new();
    private T _poolableObject;

    public void Fill(T prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            T newObject = CreateObject(prefab);
            newObject.gameObject.SetActive(false);
            _objectsInPool.Enqueue(newObject);
        }
    }

    public void Get(Vector3 positon)
    {
        T newPoolableObject;

        if (_objectsInPool.Count > 0)
        {
            newPoolableObject = _objectsInPool.Dequeue();
        }
        else
        {
            newPoolableObject = CreateObject(_poolableObject);
        }

        newPoolableObject.Disabled += Release;
        newPoolableObject.transform.position = positon;
        newPoolableObject.gameObject.SetActive(true);
    }

    private T CreateObject(T prefab) 
    {
        return Instantiate(prefab);
    }

    private void Release(T poolableObject)
    {
        _objectsInPool.Enqueue(poolableObject);
        poolableObject.Disabled -= Release;
    }
}
