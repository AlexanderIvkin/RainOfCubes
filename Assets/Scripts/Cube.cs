using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    private bool _isTouched;
    private Material _material;
    private Color _baseColor;

    public event Action<Cube> Disabled;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _baseColor = _material.color;
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched == false && collision.gameObject.GetComponent<Platform>())
        {
            ChangeColor();
            StartCoroutine(Disable(GetLifeTime()));

            _isTouched = true;
        }
    }

    private IEnumerator Disable(float delay)
    {
        yield return new WaitForSeconds(delay);

        Disabled?.Invoke(this);
        gameObject.SetActive(false);
    }

    private float GetLifeTime()
    {
        float min = 2;
        float max = 5;

        return Random.Range(min, max);
    }

    private void Init()
    {
        _isTouched = false;
        _material.color = _baseColor;
    }

    private void ChangeColor()
    {
        _material.color = new Color(Random.value, Random.value, Random.value);
    }
}
