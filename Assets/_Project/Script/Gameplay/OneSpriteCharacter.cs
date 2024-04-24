using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OneSpriteCharacter : CharacterObject
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private TMP_Text _textDispalay;
    [SerializeField] private GameObject _explosionPrefab;

    private int _isDamagedProperty;
    private void Awake()
    {
        _isDamagedProperty = Shader.PropertyToID("_IsDamaged");
    }

    public override void Setup(CharacterData characterData, CharacterGroupSetting characterGroup)
    {
        base.Setup(characterData, characterGroup);

        if (characterData.sprite != null)
            _sprite.sprite = characterData.sprite;

        if (_textDispalay != null && !string.IsNullOrEmpty(characterData.name))
            _textDispalay.text = characterData.name;
    }

    protected override void GotHitByBullet()
    {
        StartCoroutine(Co_HitBullet());
    }

    private IEnumerator Co_HitBullet()
    {
        _immune = true;
        _sprite.material.SetInteger(_isDamagedProperty, 1);

        yield return new WaitForSeconds(0.1f);
        _sprite.material.SetInteger(_isDamagedProperty, 0);
        _immune = false;

        _health--;
        if(_health<=0)
            OnDie();
    }

    protected override void OnDie()
    {
        base.OnDie();
        if (_explosionPrefab != null)
            LeanPool.Spawn(_explosionPrefab, transform.position, Quaternion.identity);

        LeanPool.Despawn(this);
    }

    protected override void ReachTarget()
    {
        base.ReachTarget();
        LeanPool.Despawn(this);
    }
}
