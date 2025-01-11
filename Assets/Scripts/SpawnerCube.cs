using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _repeatRate = 1.0f;
    [SerializeField] private float _height;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private void Start()
    {
        StartCoroutine(GetCube());
    }

    protected override void OnObjectDisabled(PoolableObject poolableObject)
    {
        base.OnObjectDisabled(poolableObject);

        _spawnerBomb.GetBomb(poolableObject.transform.position);
    }

    private IEnumerator GetCube()
    {
        bool isSpawn = true;
        var wait = new WaitForSeconds(_repeatRate);

        while (isSpawn)
        {
            yield return wait;

            PoolableObject currentCube = ObjectPooler.Get(ReturnSpawnPosition());
            currentCube.Disabled += OnObjectDisabled;
        }
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

        return new Vector3(Random.Range(leftBoundX, rightBoundX), 
            _mainPlatform.transform.position.y + _height, 
            Random.Range(leftBoundZ, rightBoundZ));
    }
}
