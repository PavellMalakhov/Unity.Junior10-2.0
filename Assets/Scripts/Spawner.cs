using UnityEngine;
using UnityEngine.Pool;
using System;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T Prefab;
    [SerializeField] private int PoolCapaciti = 95;
    [SerializeField] private int PoolMaxSize = 100;

    private ObjectPool<T> Pool;
    private int AmountAllTimeObj = 0;

    public event Action<int, int, int> PoolChanged;

    protected void Awake()
    {
        Pool = new ObjectPool<T>(
        createFunc: () => Instantiate(Prefab),
        actionOnGet: (obj) => SetActive(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: PoolCapaciti,
        maxSize: PoolMaxSize);
    }

    protected virtual void SetActive(T obj)
    {
        obj.gameObject.SetActive(true);

        AmountAllTimeObj++;

        PoolChanged?.Invoke(AmountAllTimeObj, Pool.CountAll, Pool.CountActive);
    }

    protected virtual void ReturnInPool(T gameObject)
    {
        PoolChanged?.Invoke(AmountAllTimeObj, Pool.CountAll, Pool.CountActive);
    }

    protected void GetGameObject()
    {
        Pool.Get();
    }

    protected void ReleaseGameObject(T gameObject)
    {
        Pool.Release(gameObject);
    }
}
