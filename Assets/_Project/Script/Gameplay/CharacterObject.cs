using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Notset = 0, Single = 1, Group = 2, Boss = 3, Cryer = 4, MinhAnh = 5
}

public class CharacterObject : MonoBehaviour
{
    [SerializeField] protected CharacterType type;
    [SerializeField] protected float _moveYTarget = -4f;
    [SerializeField] private GameObject _appearEffPreafab;

    protected CharacterData _characterData;
    protected CharacterGroupSetting _groupSetting;

    private float _lifeTime;
    protected int _health;
    protected bool _immune = false;
    protected bool _reachTarget = false;

    public Action<CharacterObject> onReachTarget;
    public Action<CharacterObject> onGotHitBullets;
    public Action<CharacterObject> onDie;
    public Action<CharacterObject> onDestroy;

    protected bool _initalize = false;
    private float _rootX;
    public virtual int Health => _health;
    public virtual int Score => _groupSetting.score;
    public GameObject AppearEffPrefab => _appearEffPreafab;
    public CharacterType Type => type;
    public int Id => _characterData.id;

    public virtual void Setup(CharacterData characterData, CharacterGroupSetting characterGroup)
    {
        _characterData = characterData;
        _groupSetting = characterGroup;

        _lifeTime = 0;
        _health = _groupSetting.health;
        _immune = false;
        _rootX = transform.position.x;
        _initalize = true;
    }

    protected virtual void Update()
    {
        if (!_initalize)
            return;
        MoveForward();
    }

    protected virtual void LateUpdate()
    {
        if (!_initalize)
            return;

        if (transform.position.y < _moveYTarget)
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

    public virtual void Stop()
    {
        _initalize = false;
    }

    protected virtual void OnDie()
    {
        onDie?.Invoke(this);
    }

    protected virtual void ReachTarget()
    {
        if (_reachTarget)
            return;

        _reachTarget = true;
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

    private void OnDestroy()
    {
        onDestroy?.Invoke(this);
    }
}
