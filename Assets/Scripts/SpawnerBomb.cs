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

    protected override void SetActive(Bomb bomb)
    {
        bomb.transform.position = _bombPosition;

        base.SetActive(bomb);

        bomb.Exploded += ReturnInPool;
    }

    private void GetBomb(Cube cube)
    {
        _bombPosition = cube.transform.position;

        GetGameObject();
    }

    protected override void ReturnInPool(Bomb bomb)
    {
        bomb.Exploded -= ReturnInPool;

        ReleaseGameObject(bomb);

        base.ReturnInPool(bomb);
    }
}
