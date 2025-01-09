using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public abstract class PoolableObject : MonoBehaviour
{
    public abstract event Action<PoolableObject> Disabled;

    protected Material Material;
    protected Color BaseColor;

    protected void Awake()
    {
        Material = GetComponent<Renderer>().material;
        BaseColor = Material.color;
    }

    protected float GetLifeTime()
    {
        float min = 2f;
        float max = 5f;

        return Random.Range(min, max);
    }

    protected abstract IEnumerator DelayedDisable(float delay);
}
