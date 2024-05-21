using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _mainPlatform;
    [SerializeField] private ObjectPooler _objectPooler;
    [SerializeField] private float _repeatRate = 1.0f;
    [SerializeField] private float _height;

    private void Start()
    {
        StartCoroutine(GetCube());
    }

    private Vector3 ReturnSpawnPosition()
    {
        float length = _mainPlatform.transform.localScale.x;
        float width = _mainPlatform.transform.localScale.z;
        float halfFactor = 0.5f;

        float rightBoundX = _mainPlatform.transform.position.x + length * halfFactor;
        float rightBoundZ = _mainPlatform.transform.position.z + width * halfFactor;
        float leftBoundX = _mainPlatform.transform.position.x - length * halfFactor;
        float leftBoundZ = _mainPlatform.transform.position.z - width * halfFactor;

        return new Vector3(Random.Range(leftBoundX, rightBoundX), _mainPlatform.transform.position.y + _height, Random.Range(leftBoundZ, rightBoundZ));
    }

    private IEnumerator GetCube()
    {
        bool isSpawn = true;
        var wait = new WaitForSeconds(_repeatRate);

        while (isSpawn)
        {
            yield return wait;

            _objectPooler.Get(ReturnSpawnPosition());
        }

    }
}
