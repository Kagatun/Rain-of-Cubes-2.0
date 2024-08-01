using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private Material _material;

    private Color _originalColor;

    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;

    private float _explosionRadius = 15.0f;
    private float _explosionForce = 2000;

    public event Action<Bomb> Exploded;

    public float RandomLifeTime => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);

    private void Awake()
    {
        _material = Instantiate(_material);
        GetComponent<Renderer>().material = _material;
        _originalColor = _material.color;
    }

    public void StartDisappear()
    {
        StartCoroutine(Disappear(RandomLifeTime));
    }

    private IEnumerator Disappear(float lifeTime)
    {
        Color color = _originalColor;
        float targetAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < lifeTime)
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, Time.deltaTime / lifeTime);
            elapsedTime += Time.deltaTime;
            _material.color = color;

            yield return null;
        }

        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Exploded?.Invoke(this);
    }
}
