using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;

    private ObjectPool<T> _pool;
    private int _poolCapacity = 40;
    private int _maxSize = 40;
    private int _countCreateAll = 0;

    public event Action<int, int> Spawned;

    private void Awake()
    {
        _pool = new ObjectPool<T>(CreateObject, OnGet, OnRelease, Destroy, true, _poolCapacity, _maxSize);
    }

    protected T GetFromPool()
    {
        return _pool.Get();
    }

    protected void ReleaseToPool(T obj)
    {
        _pool.Release(obj);
    }

    protected void RemoveObject(T obj)
    {
        ReleaseToPool(obj);
    }

    protected virtual T CreateObject()
    {
        return Instantiate(prefab);
    }

    protected virtual void OnGet(T obj)
    {
        _countCreateAll++;
        Spawned?.Invoke(_countCreateAll, _pool.CountActive);
        obj.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        Spawned?.Invoke(_countCreateAll, _pool.CountActive);
    }

    protected virtual void Destroy(T obj) { }
}
