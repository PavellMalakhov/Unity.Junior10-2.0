using TMPro;
using UnityEngine;

public class SpawnerView<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _poolInfo;

    private void OnEnable()
    {
        _spawner.PoolChanged += ShowInfo;
    }

    private void OnDisable()
    {
        _spawner.PoolChanged -= ShowInfo;
    }

    private void ShowInfo(PoolInfo poolInfo)
    {
        _poolInfo.text = ($"{_spawner.name}\n" +
        $"Количество заспавненых объектов за всё время (появление на сцене) = {poolInfo.AmountAllTimeObj}\n" +
        $"Количество созданных объектов = {poolInfo.PoolCountAll}\n" +
        $"Количество активных объектов на сцене = {poolInfo.PoolCountActive}");
    }
}
