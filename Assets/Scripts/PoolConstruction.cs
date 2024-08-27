using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class PoolConstruction : MonoBehaviour
{
    [SerializeField] protected GameObject Prefab;
    [SerializeField] protected int PoolCapaciti = 90;
    [SerializeField] protected int PoolMaxSize = 100;
    [SerializeField] protected TextMeshProUGUI PoolInfo;

    protected ObjectPool<GameObject> Pool;
    protected int AmountAllTimeObj = 0;

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
    }
}
