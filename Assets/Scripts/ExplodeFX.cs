using UnityEngine;

public class ExplodeFX : MonoBehaviour
{
    private void Start()
    {
        float delay = 0.3f;

        Destroy(gameObject, delay);
    }
}
