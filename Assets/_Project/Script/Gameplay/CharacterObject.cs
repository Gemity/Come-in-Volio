using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    private static readonly float _moveYTarget = -4f;

    protected CharacterData _characterData;
    protected CharacterGroupSetting _groupSetting;

    private float _lifeTime;
    protected int _health;
    protected bool _immune = false;

    public Action<CharacterObject> onReachTarget;
    public Action<CharacterObject> onGotHitBullets;
    public Action<CharacterObject> onDie;

    private float _rootX;

    public int Health => _health;
    public int Score => _groupSetting.score;

    public virtual void Setup(CharacterData characterData, CharacterGroupSetting characterGroup)
    {
        _characterData = characterData;
        _groupSetting = characterGroup;

        _lifeTime = 0;
        _health = _groupSetting.health;
        _immune = false;
        _rootX = transform.position.x;
    }

    private void Update()
    {
        MoveForward();
    }

    protected virtual void LateUpdate()
    {
        if(transform.position.y < _moveYTarget)
            ReachTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(Const.Bullet_Tag))
        {
            GotHitByBullet();
        }
    }

    protected virtual void GotHitByBullet()
    {

    }

    protected virtual void OnDie()
    {
        onDie?.Invoke(this);
    }

    protected virtual void ReachTarget()
    {
        onReachTarget?.Invoke(this);
    }

    public virtual void PlayAppearFx()
    {

    }

    private void MoveForward()
    {
        float x = _rootX;
        if (_groupSetting.speedX > 0)
        {
            x += Const.Distance_Each_Ray * Mathf.Sin(_lifeTime * _groupSetting.speedX);
        }

        float y = transform.position.y - _groupSetting.speedY * Time.deltaTime;
        transform.position = new Vector3(x, y, 0);

        _lifeTime += Time.deltaTime;
    }
}
