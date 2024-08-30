using UnityEngine;
using UnityEngine.Pool;
using System;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapaciti = 95;
    [SerializeField] private int _poolMaxSize = 100;

    private ObjectPool<T> _pool;
    private int _amountAllTimeObj = 0;

    public event Action<PoolInfo> PoolChanged;

    protected void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (obj) => SetActive(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolCapaciti,
        maxSize: _poolMaxSize);
    }

    protected virtual void SetActive(T obj)
    {
        obj.gameObject.SetActive(true);

        _amountAllTimeObj++;

        PoolChanged?.Invoke(new PoolInfo(_amountAllTimeObj, _pool.CountAll, _pool.CountActive));
    }

    protected virtual void ReturnInPool(T gameObject)
    {
        PoolChanged?.Invoke(new PoolInfo(_amountAllTimeObj, _pool.CountAll, _pool.CountActive));
    }

    protected void GetGameObject()
    {
        _pool.Get();
    }

    protected void ReleaseGameObject(T gameObject)
    {
        _pool.Release(gameObject);
    }
}
