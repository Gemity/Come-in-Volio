using Lean.Pool;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static readonly Vector2 rangeX = new Vector2(-7f, 7f);
    private static readonly Vector2 rangeY = new Vector2(-12f, 12f);

    [SerializeField] private float _speed;
    [SerializeField] private TrailRenderer _trail;

    private Vector3 _direction;
    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        transform.up = _direction;
    }

    private void Update()
    {
        transform.position += (_speed * Time.deltaTime * _direction);

        bool inSide = Utilities.InRange(rangeX, transform.position.x) && Utilities.InRange(rangeY, transform.position.y);
        if (!inSide)
        {
            DeSpawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DeSpawn();
    }

    private void DeSpawn()
    {
        LeanPool.Despawn(this);
        _trail.Clear();
    }
}
