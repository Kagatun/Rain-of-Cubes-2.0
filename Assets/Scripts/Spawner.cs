using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;
 
    protected ObjectPool<T> pool;
    private int _poolCapacity = 40;
    private int _maxSize = 40;
    private int _countCreateAll = 0;

    public event Action <int, int> OnSpawn;

    private void Awake()
    {
        pool = new ObjectPool<T>(CreateObject, ActionOnGet, OnRelease, Destroy, true, _poolCapacity, _maxSize);
    }

    protected virtual void GeObject()
    {
        pool.Get();
    }

    protected virtual T CreateObject()
    {
        T obj = Instantiate(prefab);
        return obj;
    }

    protected virtual void ActionOnGet(T obj)
    {
        _countCreateAll++;
        OnSpawn?.Invoke(_countCreateAll, pool.CountActive);
        obj.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        OnSpawn?.Invoke(_countCreateAll, pool.CountActive);
    }

    protected virtual void Destroy(T obj) { }

    protected virtual void RemoveObject(T obj)
    {
        pool.Release(obj);
    }
}
