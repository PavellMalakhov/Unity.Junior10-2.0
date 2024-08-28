using UnityEngine;
using UnityEngine.Pool;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private int PoolCapaciti = 95;
    [SerializeField] private int PoolMaxSize = 100;

    public event Action<int, int, int> PoolChanged;

    private ObjectPool<GameObject> Pool;
    private int AmountAllTimeObj = 0;

    protected void Awake()
    {
        Pool = new ObjectPool<GameObject>(
        createFunc: () => Instantiate(Prefab),
        actionOnGet: (obj) => SetActive(obj),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: PoolCapaciti,
        maxSize: PoolMaxSize);
    }

    protected virtual void SetActive(GameObject obj)
    {
        obj.SetActive(true);

        AmountAllTimeObj++;

        PoolChanged?.Invoke(AmountAllTimeObj, Pool.CountAll, Pool.CountActive);
    }

    protected virtual void ReturnInPool(GameObject gameObject)
    {
        PoolChanged?.Invoke(AmountAllTimeObj, Pool.CountAll, Pool.CountActive);
    }

    protected void GetGameObject()
    {
        Pool.Get();
    }

    protected void ReleaseGameObject(GameObject gameObject)
    {
        Pool.Release(gameObject);
    }
}
