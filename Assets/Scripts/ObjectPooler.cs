using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private int _count;

    private List<GameObject> _objects = new();
    private List<GameObject> _freeObjects = new();

    private void Awake()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject newObject = Instantiate(_object);
            newObject.SetActive(false);
            _objects.Add(newObject);
        }
    }

    private GameObject CreateObject()
    {
        return Instantiate(_object);
    }

    public void Get(Vector3 positon)
    {
        if (TryGetFreeObject(out GameObject freeObject) == false)
        {
            freeObject = CreateObject();
        }

        freeObject.transform.position = positon;
        freeObject.SetActive(true);
    }

    private bool TryGetFreeObject(out GameObject freeObject)
    {
        freeObject = null;

        _freeObjects = _objects.Where(obj => obj.active == false).ToList();

        bool isExist = _freeObjects.Count > 0;

        if (isExist)
        {
            freeObject = _freeObjects.First();
        }

        return isExist;
    }
}
