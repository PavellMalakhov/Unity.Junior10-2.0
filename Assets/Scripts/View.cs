using TMPro;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TextMeshProUGUI PoolInfo;

    private void OnEnable()
    {
        _spawner.PoolChanged += ShowInfo;
    }

    private void OnDisable()
    {
        _spawner.PoolChanged -= ShowInfo;
    }

    private void ShowInfo(int AmountAllTimeObj, int PoolCountAll, int PoolCountActive)
    {
        PoolInfo.text = ($"{_spawner.name}\n" +
        $"Количество заспавненых объектов за всё время (появление на сцене) = {AmountAllTimeObj}\n" +
        $"Количество созданных объектов = {PoolCountAll}\n" +
        $"Количество активных объектов на сцене = {PoolCountActive}");
    }
}
