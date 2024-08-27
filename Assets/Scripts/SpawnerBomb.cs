using UnityEngine;

public class SpawnerBomb : PoolConstruction
{
    [SerializeField] private Vector3 _bombPosition;
    [SerializeField] private SpawnerCube _spawnerCube;

    private void FixedUpdate()
    {
        PoolInfo.text = ($"SpawnerBomb\n" +
            $"���������� ����������� �������� �� �� ����� (��������� �� �����) = {AmountAllTimeObj}\n" +
            $"���������� ��������� �������� = {Pool.CountAll}\n" +
            $"���������� �������� �������� �� ����� = {Pool.CountActive}");
    }

    private void OnEnable()
    {
        _spawnerCube.CubeFalled += GetBomb;
    }

    private void OnDisable()
    {
        _spawnerCube.CubeFalled -= GetBomb;
    }

    protected override void SetActive(GameObject obj)
    {
        obj.transform.position = _bombPosition;

        base.SetActive(obj);

        if (obj.TryGetComponent<Bomb>(out Bomb bomb))
        {
            bomb.BombExploded += ReturnBombInPool;
        }
    }

    private void GetBomb(GameObject gameObject)
    {
        _bombPosition = gameObject.transform.position;

        Pool.Get();
    }

    private void ReturnBombInPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Bomb>(out Bomb bomb))
        {
            bomb.BombExploded -= ReturnBombInPool;
        }

        Pool.Release(gameObject);
    }
}
