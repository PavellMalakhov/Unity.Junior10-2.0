using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Renderer))]

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 300;
    [SerializeField] private float _explosionRadius = 2;
    [SerializeField] private Renderer _renderer;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        float delayMin = 2f;
        float delayMax = 5f;
        float delay = UnityEngine.Random.Range(delayMin, delayMax);

        StartCoroutine(Timer(delay));
    }

    public void Explode()
    {
        Collider[] cubeshits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (var hit in cubeshits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        foreach (var cube in cubes)
        {
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Exploded?.Invoke(this);
    }

    private IEnumerator Timer(float delay)
    {
        var wait = new WaitForEndOfFrame();

        float colorChannelAMax = 1f;
        float colorChannelA = 1f;

        while (_renderer.material.color.a > 0)
        {
            _renderer.material.color = new Color(0, 0, 0, colorChannelA -= colorChannelAMax * Time.deltaTime / delay);

            yield return wait;
        }

        _renderer.material.color = new Color(255, 0, 0, 1);
        yield return wait;

        _renderer.material.color = new Color(0, 0, 0, 1);

        Explode();
    }
}
