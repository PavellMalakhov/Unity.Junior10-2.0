using System;
using System.Collections;
using UnityEngine;

public class SpawnerCube : Attribute
{
    public event Action<GameObject> CubeFalled;

    private void Start()
    {
        float cubeRepeatTime = 0.05f;

        StartCoroutine(RepeatGetCube(cubeRepeatTime));
    }

    private void FixedUpdate()
    {
        PoolInfo.text = ($"SpawnerCube\n" +
            $"Количество заспавненых объектов за всё время (появление на сцене) = {AmountAllTimeObj}\n" +
            $"Количество созданных объектов = {Pool.CountAll}\n" +
            $"Количество активных объектов на сцене = {Pool.CountActive}");
    }

    private IEnumerator RepeatGetCube(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Pool.Get();

            yield return wait;
        }
    }

    protected override void FallAndExplode(GameObject obj)
    {
        float cloudSize = 5f;
        float cloudHeight = 9f;

        obj.transform.position = new Vector3(
            UnityEngine.Random.Range(-cloudSize, cloudSize), cloudHeight, 
            UnityEngine.Random.Range(-cloudSize, cloudSize));

        base.FallAndExplode(obj);

        if (obj.TryGetComponent<Cube>(out Cube cube))
        {
            cube.Falled += ReturnCubeInPool;
        }
    }

    private void ReturnCubeInPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            cube.Falled -= ReturnCubeInPool;
        }

        CubeFalled?.Invoke(gameObject);
        
        Pool.Release(gameObject);
    }
}
