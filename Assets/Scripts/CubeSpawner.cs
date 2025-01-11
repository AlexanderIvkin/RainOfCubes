using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _delay;
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _height;
    [SerializeField] private BombSpawner _bombSpawner;

    private void Start()
    {
        StartCoroutine(SpawningCubes());
    }

    private IEnumerator SpawningCubes()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            Spawn(ReturnSpawnPosition());
        }
    }

    protected override void OnObjectDisable(PoolableObject poolableObject)
    {
        base.OnObjectDisable(poolableObject);

        _bombSpawner.Spawn(poolableObject.transform.position);
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
