using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : PoolableObject<Cube>
{
    private bool _isTouched;

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

        Disable(this);
        gameObject.SetActive(false);
    }

    private void ChangeColor()
    {
        Material.color = new Color(Random.value, Random.value, Random.value);
    }

    protected override void Init()
    {
        Name = " Û·";
        _isTouched = false;
        Material.color = BaseColor;
    }
}
