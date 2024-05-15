using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Cube _objectPrefab;
    [SerializeField] private int _count;

    private List<Cube> _objects = new();
    private List<Cube> _freeObjects = new();

    private void Awake()
    {
        for (int i = 0; i < _count; i++)
        {
            Cube newCube = CreateObject();
            newCube.gameObject.SetActive(false);
            _objects.Add(newCube);
        }
    }

    private Cube CreateObject()
    {
        return Instantiate(_objectPrefab);
    }

    public void Get(Vector3 positon)
    {
        if (TryGetFreeObject(out Cube freeObject) == false)
        {
            freeObject = CreateObject();
        }

        freeObject.transform.position = positon;
        freeObject.gameObject.SetActive(true);
    }

    private bool TryGetFreeObject(out Cube freeObject)
    {
        freeObject = null;

        _freeObjects = _objects.Where(obj => obj.gameObject.active == false).ToList();

        bool isExist = _freeObjects.Count > 0;

        if (isExist)
        {
            freeObject = _freeObjects.First();
        }

        return isExist;
    }
}
