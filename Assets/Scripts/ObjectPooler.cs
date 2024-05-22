using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Cube _objectPrefab;
    [SerializeField] private int _capacity;

    private Queue<Cube> _objectsInPool = new();

    private void Awake()
    {
        for (int i = 0; i < _capacity; i++)
        {
            Cube newCube = CreateObject();
            newCube.gameObject.SetActive(false);
            _objectsInPool.Enqueue(newCube);
        }
    }

    public void Get(Vector3 positon)
    {
        Cube newCube;

        if (_objectsInPool.Count > 0)
        {
            newCube = _objectsInPool.Dequeue();
        }
        else
        {
            newCube = CreateObject();
        }

        newCube.Disabled += Release;
        newCube.transform.position = positon;
        newCube.gameObject.SetActive(true);
    }

    private Cube CreateObject()
    {
        return Instantiate(_objectPrefab);
    }

    private void Release(Cube cube)
    {
        _objectsInPool.Enqueue(cube);
        cube.Disabled -= Release;
    }
}
