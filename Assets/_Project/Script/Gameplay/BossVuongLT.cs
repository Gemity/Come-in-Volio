using DG.Tweening;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class BossVuongLT : CharacterObject
{
    [SerializeField] private Vector3[] _path;
    [SerializeField] private Animation _animation;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private CryerFollowBoss[] _cryersFollow;

    private const string IdleAnim = "BossIdle", MoveAnim = "BossMove";
    private int _isDamagedProperty;
    private float _percentPerHp;
    private Coroutine _bossMoveRoutine;

    private void Start()
    {
        _canvas.worldCamera = Camera.main;
        float time = (transform.position.y - _moveYTarget) / _groupSetting.speedY;
        transform.DOMoveY(_moveYTarget, time).SetEase(Ease.Linear).OnComplete(() => ReachTargetBoss());
    }

    private void Awake()
    {
        _isDamagedProperty = Shader.PropertyToID("_IsDamaged");
    }

    public override void Setup(CharacterData characterData, CharacterGroupSetting characterGroup)
    {
        base.Setup(characterData, characterGroup);
        _percentPerHp = 1f / characterGroup.health;
    }

    protected override void Update()
    {

    }

    protected override void LateUpdate()
    {

    }

    protected override void GotHitByBullet()
    {
        StartCoroutine(Co_HitBullet());
    }

    private IEnumerator Co_HitBullet()
    {
        _immune = true;
        _sprite.material.SetInteger(_isDamagedProperty, 1);
        _healthBar.DOFillAmount(_healthBar.fillAmount - _percentPerHp, 0.15f);
        StartCoroutine(GameplayController.Instance.Co_Splash());
        yield return new WaitForSeconds(0.1f);
        _sprite.material.SetInteger(_isDamagedProperty, 0);
        _immune = false;

        _health--;
        if (_health <= 0)
            OnDie();
    }

    protected override void OnDie()
    {
        base.OnDie();
        if (_bossMoveRoutine != null)
            StopCoroutine(_bossMoveRoutine);

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        StartCoroutine(Co_Die());
    }

    private IEnumerator Co_Die()
    {
        _sprite.enabled = false;
        _canvas.gameObject.SetActive(false);
        foreach(var i in _cryersFollow)
            i.Anim.Stop();

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void ReachTargetBoss()
    {
        if (_reachTarget)
            return;

        _reachTarget = true;
        onReachTarget?.Invoke(this);
        _bossMoveRoutine = StartCoroutine(Co_ReachTarget());
    }

    private IEnumerator Co_ReachTarget()
    {
        WaitForSeconds wait_3s = new WaitForSeconds(3);

        while(true)
        {
            _animation.Play(IdleAnim);
            yield return wait_3s;
            _animation.Play(MoveAnim);
            for (int i = 0; i < _path.Length; i++)
            {
                yield return transform.DOMove(_path[i], 2).From(transform.position).SetEase(Ease.Linear).WaitForCompletion();
            }
        }
    }

    public override void Stop()
    {
        base.Stop();
        _animation.Stop();
        if (_bossMoveRoutine != null)
            StopCoroutine(_bossMoveRoutine);
    }
}
