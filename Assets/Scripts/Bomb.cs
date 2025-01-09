using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PoolableObject
{
    [SerializeField] private float _explotionRadius;
    [SerializeField] private float _explotionForce;

    public override event Action<PoolableObject> Disabled;

    private void OnEnable()
    {
        Material.color = BaseColor;
        StartCoroutine(DelayedDisable(GetLifeTime()));
    }

    protected override IEnumerator DelayedDisable(float delay)
    {
        float fullAlpha = 1f;
        float currentTime = delay;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            float normalizedValue = Mathf.Clamp(currentTime / delay, 0f, fullAlpha);
            Material.color = new Color(Material.color.r,
                Material.color.g,
                Material.color.b,
                normalizedValue);

            yield return null;
        }

        Explode();
        Disabled?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explotionRadius);

        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out Rigidbody rigidbody))
                {
                    rigidbody.AddExplosionForce(_explotionForce, transform.position, _explotionRadius);
                }
            }
        }
    }
}
