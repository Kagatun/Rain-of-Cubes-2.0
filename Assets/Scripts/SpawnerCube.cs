using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private SpawnerBomb _spawnerBomb;

    private float _repeatRate = 0.3f;

    private void Start()
    {
        InvokeRepeating(nameof(GeObject), 0.0f, _repeatRate);
    }

    protected override void GeObject()
    {
        base.GeObject();
    }

    protected override Cube CreateObject()
    {
        Cube cube = Instantiate(prefab);
        cube.Touched += RemoveObject;
        cube.Destroyed += _spawnerBomb.SpawnBomb;
        return cube;
    }

    protected override void ActionOnGet(Cube cube)
    {
        float minStartPosition = -45.0f;
        float maxStartPosition = 45.0f;
        float positionX = Random.Range(minStartPosition, maxStartPosition);
        float positionY = Random.Range(minStartPosition, maxStartPosition);

        Vector3 positionStart = transform.position + new Vector3(positionX, 0, positionY);
        cube.transform.position = positionStart;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.SetStartingColor();

        base.ActionOnGet(cube);
    }

    protected override void OnRelease(Cube cube)
    {
        cube.ClearTouch();
        base.OnRelease(cube);
    }

    protected override void Destroy(Cube cube)
    {
        cube.Touched -= RemoveObject;
        cube.Destroyed -= _spawnerBomb.SpawnBomb;
    }

    protected override void RemoveObject(Cube cube)
    {
        base.RemoveObject(cube);
    }
}
