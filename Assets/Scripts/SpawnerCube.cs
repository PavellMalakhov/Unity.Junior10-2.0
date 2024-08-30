using System;
using System.Collections;
using UnityEngine;


public class SpawnerCube : Spawner<Cube>
{
    private Transform _transformStart;

    public event Action<Cube> CubeFalled;

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

    protected override void SetActive(Cube cube)
    {
        _transformStart = cube.transform;

        float cloudSize = 5f;
        float cloudHeight = 9f;

        cube.transform.position = new Vector3(
            UnityEngine.Random.Range(-cloudSize, cloudSize), cloudHeight, 
            UnityEngine.Random.Range(-cloudSize, cloudSize));

        base.SetActive(cube);

        cube.Falled += ReturnInPool;
    }

    protected override void ReturnInPool(Cube cube)
    {
        cube.Falled -= ReturnInPool;

        CubeFalled?.Invoke(cube);

        ResetStatus(cube);

        ReleaseGameObject(cube);

        base.ReturnInPool(cube);
    }

    private void ResetStatus(Cube cube)
    {
        cube.transform.SetPositionAndRotation(_transformStart.position, _transformStart.rotation);

        cube.Rigidbody.velocity = Vector3.zero;
        cube.Rigidbody.angularVelocity = Vector3.zero;
    }
}
