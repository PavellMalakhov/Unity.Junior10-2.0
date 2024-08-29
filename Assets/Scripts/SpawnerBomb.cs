using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private Vector3 _bombPosition;
    [SerializeField] private SpawnerCube _spawnerCube;

    private void OnEnable()
    {
        _spawnerCube.CubeFalled += GetBomb;
    }

    private void OnDisable()
    {
        _spawnerCube.CubeFalled -= GetBomb;
    }

    protected override void SetActive(Bomb obj)
    {
        obj.transform.position = _bombPosition;

        base.SetActive(obj);

        //if (obj.TryGetComponent<Bomb>(out Bomb bomb))
        //{
        //    bomb.BombExploded += ReturnInPool;
        //}

        obj.BombExploded += ReturnInPool;
    }

    private void GetBomb(Cube gameObject)
    {
        _bombPosition = gameObject.transform.position;

        GetGameObject();
    }

    protected override void ReturnInPool(Bomb gameObject)
    {
        //if (gameObject.TryGetComponent<Bomb>(out Bomb bomb))
        //{
        //    bomb.BombExploded -= ReturnInPool;
        //}

        gameObject.BombExploded -= ReturnInPool;

        ReleaseGameObject(gameObject);

        base.ReturnInPool(gameObject);
    }
}
