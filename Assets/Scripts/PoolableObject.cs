using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public abstract class PoolableObject : MonoBehaviour
{
    public event Action<PoolableObject> Disabled;

    protected Material Material;
    protected Color BaseColor;

    protected void Awake()
    {
        Material = GetComponent<Renderer>().material;
        BaseColor = Material.color;
    }

    protected void OnEnable()
    {
        Init();
    }

    protected float GetLifeTime()
    {
        float min = 2f;
        float max = 5f;

        return Random.Range(min, max);
    }

    protected void Disable()
    {
        Disabled?.Invoke(this);
    }

    protected abstract IEnumerator DelayedDisable(float delay);

    protected abstract void Init();
}
