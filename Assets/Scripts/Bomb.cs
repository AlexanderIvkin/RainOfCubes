using System.Collections;
using UnityEngine;

public class Bomb : PoolableObject
{
    [SerializeField] private float _explotionRadius;
    [SerializeField] private float _explotionForce;
    [SerializeField] private ExplodeFX _explodeFX;

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
        Disable();
        gameObject.SetActive(false);
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explotionRadius);
        Instantiate(_explodeFX, gameObject.transform.position, Quaternion.identity);

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

    protected override void Init()
    {
        Material.color = BaseColor;
        StartCoroutine(DelayedDisable(GetLifeTime()));
    }
}
