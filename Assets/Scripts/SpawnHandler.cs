using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] private float _cubeSpawnTime;
    [SerializeField] private SpawnerCube _spawnerCube;
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private bool _isNeedCube = false;
    private bool _isNeedBomb = false;

    private IEnumerator Timer()
    {
        bool isEnable = true;
        var wait = new WaitForSecondsRealtime(_cubeSpawnTime);

        while (isEnable)
        {
            _isNeedCube = 
        }
    }
}
