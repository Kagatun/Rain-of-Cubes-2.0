using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void SpawnBomb(Vector3 position)
    {
        Bomb bomb = pool.Get();
        bomb.transform.position = position;
    }

    protected override Bomb CreateObject()
    {
        Bomb bomb = Instantiate(prefab);
        bomb.Exploded += RemoveObject;
        return bomb;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);
        bomb.StartDisappear();
    }

    protected override void OnRelease(Bomb bomb)
    {
        base.OnRelease(bomb);
    }

    protected override void Destroy(Bomb bomb)
    {
        bomb.Exploded -= RemoveObject;
        Destroy(bomb.gameObject);
    }

    protected override void RemoveObject(Bomb bomb)
    {
        base.RemoveObject(bomb);
    }
}