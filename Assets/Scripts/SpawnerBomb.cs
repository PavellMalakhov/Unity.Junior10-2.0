using UnityEngine;

public class SpawnerBomb : BasePool
{
    [SerializeField] private Vector3 _bombPosition;

    private void FixedUpdate()
    {
        PoolInfo.text = ($"SpawnerBomb\n" +
            $"���������� ����������� �������� �� �� ����� (��������� �� �����) = {AmountAllTimeObj}\n" +
            $"���������� ��������� �������� = {Pool.CountAll}\n" +
            $"���������� �������� �������� �� ����� = {Pool.CountActive}");
    }

    private void OnEnable()
    {
        Cube.CubeFalled += GetBomb;
        Bomb.BombExploded += ReturnBomb;
    }

    private void OnDisable()
    {
        Cube.CubeFalled -= GetBomb;
        Bomb.BombExploded -= ReturnBomb;
    }

    protected override void ActionOnGet(GameObject obj)
    {
        obj.transform.position = _bombPosition;

        base.ActionOnGet(obj);
    }

    private void GetBomb(GameObject gameObject)
    {
        _bombPosition = gameObject.transform.position;

        Pool.Get();
    }

    private void ReturnBomb(GameObject gameObject)
    {
        Pool.Release(gameObject);
    }
}
