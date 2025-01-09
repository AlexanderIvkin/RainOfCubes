using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : PoolableObject
{
    private bool _isTouched;

    public override event Action<PoolableObject> Disabled;

    private void OnEnable()
    {
        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched == false && collision.gameObject.GetComponent<Platform>())
        {
            ChangeColor();
            StartCoroutine(DelayedDisable(GetLifeTime()));

            _isTouched = true;
        }
    }

    protected override IEnumerator DelayedDisable(float delay)
    {
        yield return new WaitForSeconds(delay);

        Disabled?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void Init()
    {
        _isTouched = false;
        Material.color = BaseColor;
    }

    private void ChangeColor()
    {
        Material.color = new Color(Random.value, Random.value, Random.value);
    }
}
