using UnityEngine;

public class SpawnerBomb : Spawner
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

    protected override void SetActive(GameObject obj)
    {
        obj.transform.position = _bombPosition;

        base.SetActive(obj);

        if (obj.TryGetComponent<Bomb>(out Bomb bomb))
        {
            bomb.BombExploded += ReturnInPool;
        }
    }

    private void GetBomb(GameObject gameObject)
    {
        _bombPosition = gameObject.transform.position;

        GetGameObject();
    }

    protected override void ReturnInPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<Bomb>(out Bomb bomb))
        {
            bomb.BombExploded -= ReturnInPool;
        }

        ReleaseGameObject(gameObject);

        base.ReturnInPool(gameObject);
    }
}
