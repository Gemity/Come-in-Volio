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
    private float _speedX, _speedY;
    protected bool _immune = false;

    public Action<CharacterObject> onReachTarget;
    public Action<CharacterObject> onGotHitBullets;
    public Action<CharacterObject> onDie;

    public void Setup(CharacterData characterData, int group)
    {
        _characterData = characterData;
        _groupSetting = CharacterGroupSettingSO.Instance.GetCharacterGroupById(group);

        _lifeTime = 0;
        _health = _groupSetting.health;

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
        float dirX = Mathf.Sin(_lifeTime) > 0 ? 1 : -1;
        float x = transform.position.x + dirX * _speedX * Time.deltaTime;
        float y = transform.position.y -  _speedY * Time.deltaTime;
        transform.position = new Vector3(x, y, 0);

        _lifeTime += Time.deltaTime;
    }
}
