using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : PoolableObject
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

        Disable();
        gameObject.SetActive(false);
    }

    private void ChangeColor()
    {
        Material.color = new Color(Random.value, Random.value, Random.value);
    }

    protected override void Init()
    {
        _isTouched = false;
        Material.color = BaseColor;
    }
}
