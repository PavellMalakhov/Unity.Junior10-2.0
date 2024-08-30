using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private bool _isPlatformHit = false;

    public event Action<Cube> Falled;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        if (this.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            Rigidbody = rigidbody;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Platform>(out _) && _isPlatformHit == false)
        {
            _isPlatformHit = true;

            ChangeState();
        }
    }

    private void ChangeState()
    {
        _renderer.material.color = GetRandomColor();

        float lifetimeMinCube = 2f;
        float lifetimeMaxCube = 5f;
        float delay = UnityEngine.Random.Range(lifetimeMinCube, lifetimeMaxCube);
        Color cubeStartColor = new(1, 1, 1);

        StartCoroutine(CountUp(delay, cubeStartColor));
    }

    private IEnumerator CountUp(float delay, Color cubeStartColor)
    {
        var wait = new WaitForSeconds(delay);

        yield return wait;

        _renderer.material.color = cubeStartColor;

        _isPlatformHit = false;

        Falled?.Invoke(this);
    }

    private Color GetRandomColor()
    {
        float colorChannelR = UnityEngine.Random.value;
        float colorChannelG = UnityEngine.Random.value;
        float colorChannelB = UnityEngine.Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
