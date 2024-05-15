using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _mainPlatform;
    [SerializeField] private float _repeatRate = 1.0f;
    [SerializeField] private float _height;
    [SerializeField] private ObjectPooler _objectPooler;

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 1.0f, _repeatRate);
    }

    private Vector3 ReturnPosition()
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

    private void GetCube()
    {
        _objectPooler.Get(ReturnPosition());
    }
}
