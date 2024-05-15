using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private bool _isTouched;
    private Material _material;
    private Color _baseColor;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _baseColor = _material.color;
    }

    private void OnEnable()
    {
        _isTouched = false;
        _material.color = _baseColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched == false && collision.gameObject.GetComponent<Platform>())
        {
            ChangeColor();
            StartCoroutine(Deactivation(GetLifeTime()));

            _isTouched = true;
        }
    }

    private IEnumerator Deactivation(int delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }

    private void ChangeColor()
    {
        _material.color = new Color(Random.value, Random.value, Random.value);
    }

    private int GetLifeTime()
    {
        int min = 2;
        int max = 5;

        return Random.Range(min, max);
    }
}
