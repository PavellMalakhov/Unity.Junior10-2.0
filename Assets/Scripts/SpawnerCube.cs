using UnityEngine;

public class SpawnerCube : BasePool
{
    private void Start()
    {
        float rainStartTime = 0f;
        float cubeRepeatTime = 0.05f;

        InvokeRepeating(nameof(GetCube), rainStartTime, cubeRepeatTime);
    }

    private void FixedUpdate()
    {
        PoolInfo.text = ($"SpawnerCube\n" +
            $"Количество заспавненых объектов за всё время (появление на сцене) = {AmountAllTimeObj}\n" +
            $"Количество созданных объектов = {Pool.CountAll}\n" +
            $"Количество активных объектов на сцене = {Pool.CountActive}");
    }

    private void OnEnable()
    {
        Cube.CubeFalled += ReturnCubeInCloud;
    }

    private void OnDisable()
    {
        Cube.CubeFalled -= ReturnCubeInCloud;
    }

    protected override void ActionOnGet(GameObject obj)
    {
        float cloudSize = 5f;
        float cloudHeight = 9f;

        obj.transform.position = new Vector3(Random.Range(-cloudSize, cloudSize), cloudHeight, Random.Range(-cloudSize, cloudSize));

        base.ActionOnGet(obj);
    }

    private void GetCube()
    {
        Pool.Get();
    }

    private void ReturnCubeInCloud(GameObject gameObject)
    {
        Pool.Release(gameObject);
    }
}
