using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    private static readonly float _moveYTarget = -4f;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private BoxCollider2D _boxCollider;

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

    public void Setup(CharacterData characterData, CharacterGroupSetting characterGroup)
    {
        _characterData = characterData;
        _groupSetting = characterGroup;

        _lifeTime = 0;
        _health = _groupSetting.health;
        _sprite.sprite = _characterData.sprite;

        _boxCollider.offset = new Vector2(0, 0);
        _boxCollider.size = new Vector2(_sprite.bounds.size.x / transform.lossyScale.x, _sprite.bounds.size.y / transform.lossyScale.y);

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
        onGotHitBullets?.Invoke(this);
        _health--;
        if(_health == 0)
            OnDie();
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
