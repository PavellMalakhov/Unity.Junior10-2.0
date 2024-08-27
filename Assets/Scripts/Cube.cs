using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    public event Action<GameObject> Falled;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Init();
    }

    private void Init()
    {
        GetComponent<Renderer>().material.color = GetRandomColor();

        float lifetimeMinCube = 2f;
        float lifetimeMaxCube = 5f;
        float delay = UnityEngine.Random.Range(lifetimeMinCube, lifetimeMaxCube);
        Color cubeStartColor = new (1, 1, 1);

        StartCoroutine(CountUp(delay, cubeStartColor));
    }

    private IEnumerator CountUp(float delay, Color cubeStartColor)
    {
        var wait = new WaitForSeconds(delay);
        
        yield return wait;

        _renderer.material.color = cubeStartColor;

        Falled?.Invoke(gameObject);
    }

    private Color GetRandomColor()
    {
        float colorChannelR = UnityEngine.Random.value;
        float colorChannelG = UnityEngine.Random.value;
        float colorChannelB = UnityEngine.Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
