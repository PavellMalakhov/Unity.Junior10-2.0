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
        $"���������� ����������� �������� �� �� ����� (��������� �� �����) = {poolInfo.AmountAllTimeObj}\n" +
        $"���������� ��������� �������� = {poolInfo.PoolCountAll}\n" +
        $"���������� �������� �������� �� ����� = {poolInfo.PoolCountActive}");
    }
}
