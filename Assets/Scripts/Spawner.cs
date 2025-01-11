using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T: PoolableObject
{
    [SerializeField] protected ObjectPooler<T> ObjectPooler;


    public int SpawnedObjectsCount { get; protected set; } = 0;
    public int CreatedObjectsCount => ObjectPooler.CreatedObjectsCount;



    protected virtual void OnObjectDisabled(PoolableObject currentObject)
    {
        ObjectPooler.Release((T)currentObject);
        currentObject.Disabled -= OnObjectDisabled;
    }
}
