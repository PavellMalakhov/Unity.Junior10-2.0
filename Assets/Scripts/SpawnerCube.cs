using System;
using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner
{
    public event Action<GameObject> CubeFalled;

    private Transform _transformStart;

    private void Start()
    {
        float cubeRepeatTime = 0.05f;

        StartCoroutine(RepeatGetCube(cubeRepeatTime));
    }

    private IEnumerator RepeatGetCube(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            GetGameObject();

            yield return wait;
        }
    }

    protected override void SetActive(GameObject obj)
    {
        _transformStart = obj.transform;

        float cloudSize = 5f;
        float cloudHeight = 9f;

        obj.transform.position = new Vector3(
            UnityEngine.Random.Range(-cloudSize, cloudSize), cloudHeight, 
            UnityEngine.Random.Range(-cloudSize, cloudSize));

        base.SetActive(obj);

        if (obj.TryGetComponent<Cube>(out Cube cube))
        {
            cube.Falled += ReturnInPool;
        }
    }

    protected override void ReturnInPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            cube.Falled -= ReturnInPool;
        }

        CubeFalled?.Invoke(gameObject);

        ResetStatus(gameObject);

        ReleaseGameObject(gameObject);

        base.ReturnInPool(gameObject);
    }

    private void ResetStatus(GameObject gameObject)
    {
        gameObject.transform.SetPositionAndRotation(_transformStart.position, _transformStart.rotation);

        if (gameObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
