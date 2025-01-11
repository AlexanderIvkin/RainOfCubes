using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    private List<Bomb> ActiveBombs = new();

    public void GetBomb(Vector3 position)
    {
        Bomb currentBomb = ObjectPooler.Get(position);
        ActiveBombs.Add(currentBomb);
        currentBomb.Disabled += OnBombDisabled;
    }

    private void OnBombDisabled(Bomb bomb)
    {
        ActiveBombs.Remove(bomb);
        ObjectPooler.Release(bomb);
        bomb.Disabled -= OnBombDisabled;
    }
}
