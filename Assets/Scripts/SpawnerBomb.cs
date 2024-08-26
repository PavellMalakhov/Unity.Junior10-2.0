using UnityEngine;

public class SpawnerBomb : Attribute
{
    [SerializeField] private Vector3 _bombPosition;
    [SerializeField] private SpawnerCube _spawnerCube;

    private void FixedUpdate()
    {
        PoolInfo.text = ($"SpawnerBomb\n" +
            $"Количество заспавненых объектов за всё время (появление на сцене) = {AmountAllTimeObj}\n" +
            $"Количество созданных объектов = {Pool.CountAll}\n" +
            $"Количество активных объектов на сцене = {Pool.CountActive}");
    }

    private void OnEnable()
    {
        _spawnerCube.CubeFalled += GetBomb;
    }

    private void OnDisable()
    {
        _spawnerCube.CubeFalled -= GetBomb;
    }

    protected override void FallAndExplode(GameObject obj)
    {
        obj.transform.position = _bombPosition;

        base.FallAndExplode(obj);

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
