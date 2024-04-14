using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _gunAnchor;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _coolDownFire = 0.5f;

    private float _timerCooldown;
    private bool _enableFire = false;
    public bool EnableFire { get => _enableFire; set => _enableFire = value; }

    public void Init()
    {
        _enableFire = true;
        _timerCooldown = 0f;
        InputControl.Instance.onFingerDown += OnFingerDown;
    }

    private void OnFingerDown(Vector3 vector)
    {
        Vector3 dir = (vector - _gunAnchor.position).normalized;
        if(Vector2.Angle(dir, Vector3.up) < 80)
        {
            _gunAnchor.transform.up = dir;
            if (_enableFire && _timerCooldown > _coolDownFire)
            {
                Bullet bullet = LeanPool.Spawn<Bullet>(_bulletPrefab, _firePoint.position, Quaternion.identity);
                bullet.SetDirection(dir);
                _timerCooldown = 0f;
            }
        }
    }

    private void Update()
    {
        _timerCooldown += Time.deltaTime;
    }
}
