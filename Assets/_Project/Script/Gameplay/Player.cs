using DG.Tweening;
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
    [SerializeField] private Transform _camera;
    [SerializeField] private Animation _muzzle;
    [SerializeField] private SpriteRenderer _hintDir;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _bulletFalling;

    private float _timerCooldown;
    private bool _enableFire = false;
    public bool EnableFire { get => _enableFire; set => _enableFire = value; }
    private readonly Vector3 _bulletFallingPos = new Vector3(0.11f, -7.09f, 0);

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
                _camera.DOShakePosition(0.2f, 6);
                _muzzle.Play();
                _hintDir.DOFade(0, 0.2f).OnComplete(() => _hintDir.color = Vector4.one);
                _target.transform.position = vector;
                LeanPool.Spawn(_bulletFalling);

                if(User.Sound)
                    AudioManager.Instance.PlaySfx("Shoot");
            }
        }
    }

    private void Update()
    {
        _timerCooldown += Time.deltaTime;
    }
}
