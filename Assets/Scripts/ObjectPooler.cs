using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Cube _objectPrefab;
    [SerializeField] private int _capacity;

    private List<Cube> _subscribesCubes = new();
    private Queue<Cube> _objectsInPool = new();

    private void Awake()
    {
        for (int i = 0; i < _capacity; i++)
        {
            Cube newCube = CreateObject();
            newCube.gameObject.SetActive(false);
            _objectsInPool.Enqueue(newCube);
            _subscribesCubes.Add(newCube);
        }
    }

    private void OnEnable()
    {
        foreach (var cube in _objectsInPool)
        {
            cube.Disabled += Release;
        }
    }

    private void OnDisable()
    {
        foreach (var cube in _subscribesCubes)
        {
            cube.Disabled -= Release;
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
            newCube.Disabled += Release;
            _subscribesCubes.Add(newCube);
        }

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
    }
}
