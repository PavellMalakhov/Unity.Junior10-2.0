using TMPro;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private Spawner<Cube> _spawner;
    [SerializeField] private TextMeshProUGUI _poolInfo;

    private void OnEnable()
    {
        _spawner.PoolChanged += ShowInfo;
    }

    private void OnDisable()
    {
        _spawner.PoolChanged -= ShowInfo;
    }

    private void ShowInfo(int amountAllTimeObj, int poolCountAll, int poolCountActive)
    {
        _poolInfo.text = ($"{_spawner.name}\n" +
        $"���������� ����������� �������� �� �� ����� (��������� �� �����) = {amountAllTimeObj}\n" +
        $"���������� ��������� �������� = {poolCountAll}\n" +
        $"���������� �������� �������� �� ����� = {poolCountActive}");
    }
}
