using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacter : CharacterObject
{
    [SerializeField] private List<SpriteRenderer> _hitEffectSprite;

    protected override void GotHitByBullet()
    {
        StartCoroutine(Co_HitBullet());
    }

    private IEnumerator Co_HitBullet()
    {
        _immune = true;
        Color color = _hitEffectSprite[0].color;
        foreach (var sprite in _hitEffectSprite)
        {
            sprite.color = Color.white;
        }

        yield return new WaitForSeconds(0.1f);
        foreach (var sprite in _hitEffectSprite)
        {
            sprite.color = color;
        }
        yield return null;
        _immune = false;

        base.GotHitByBullet();
    }

    protected override void OnDie()
    {
        base.OnDie();
        LeanPool.Despawn(this);
    }

    protected override void ReachTarget()
    {
        base.ReachTarget();
        LeanPool.Despawn(this);
    }
}
