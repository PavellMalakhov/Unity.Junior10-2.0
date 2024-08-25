using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class BasePool : MonoBehaviour
{
    [SerializeField] protected GameObject Prefab;
    [SerializeField] protected int PoolCapaciti = 40;
    [SerializeField] protected int PoolMaxSize = 100;
    [SerializeField] protected TextMeshProUGUI PoolInfo;

    protected ObjectPool<GameObject> Pool;
    protected int AmountAllTimeObj = 0;

    protected void Awake()
    {
        Pool = new ObjectPool<GameObject>(
        createFunc: () => Instantiate(Prefab),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: PoolCapaciti,
        maxSize: PoolMaxSize);
    }

    protected virtual void ActionOnGet(GameObject obj)
    {
        obj.SetActive(true);

        AmountAllTimeObj++;
    }
}
