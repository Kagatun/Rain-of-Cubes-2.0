using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private LayerMask _platform;

    private Renderer _renderer;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;
    private bool _isTouched = false;

    public event Action<Cube> Touched;
    public event Action<Vector3> Destroyed;

    public float RandomLifeTime => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & _platform) != 0)
        {
            if (!_isTouched)
            {
                Touch();
                ChangeColor();
                StartCoroutine(ReleaseAfterTime(RandomLifeTime));
            }
        }
    }

    public void ClearTouch()
    {
        _isTouched = false;
    }

    public void SetStartingColor()
    {
        _renderer.material.color = Color.red;
    }

    private void Destroy()
    {
        Destroyed?.Invoke(transform.position);
        Touched?.Invoke(this);
    }

    private void ChangeColor()
    {
        _renderer.material.color = Color.blue;
    }

    private void Touch()
    {
        _isTouched = true;
    }

    private IEnumerator ReleaseAfterTime(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Destroy();
    }
}
