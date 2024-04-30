using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using DG.Tweening;
using UnityEngine.UI;

public class CompleteGameController : Controller
{
    public const string COMPLETEGAME_SCENE_NAME = "CompleteGame";

    public override string SceneName()
    {
        return COMPLETEGAME_SCENE_NAME;
    }

    [SerializeField] private GameObject _lightEff;
    [SerializeField] private GameObject _confetti;
    [SerializeField] private GameObject _nextBtn;
    [SerializeField] private RectTransform _get1Bil, _get5mPerMonth;
    [SerializeField] private RectTransform _reciveReward;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _chungNhan;
    [SerializeField] private Transform[] _extraGet1Bill;

    private Tween _tweener;
    private List<Tween> _tweenersExtra;

    private int _countClick1Bil = 0;
    private IEnumerator Start()
    {
        _tweenersExtra = new List<Tween> ();
        if (User.Sound)
            AudioManager.Instance.PlaySfx("poster win");
        yield return new WaitForSeconds(0.75f);
        _lightEff.SetActive(true);
        _confetti.SetActive(true);

        yield return _nextBtn.transform.DOScale(1, 0.4f).From(0).SetEase(Ease.OutBack);
    }

    public void Next()
    {
        StartCoroutine(Co_Next());
    }

    private IEnumerator Co_Next()
    {
        if (User.Sound)
            AudioManager.Instance.PlaySfx("Pop-up fade");
        _lightEff.SetActive(false);
        AudioManager.Instance.StopBgm();
        yield return _canvasGroup.DOFade(0,0.4f).WaitForCompletion();
        yield return _reciveReward.DOAnchorPosY(0, 0.4f).SetEase(Ease.OutBack);
        if (User.Sound)
            AudioManager.Instance.PlaySfx("getMoney");
        _tweener = _get1Bil.DOScale(1, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _get1Bil.DOScale(1.1f, 0.3f).From(1).SetLoops(-1, LoopType.Yoyo);
        });
        _get5mPerMonth.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    public void Get1Bil()
    {
        _countClick1Bil++;
        if (_countClick1Bil == 3)
        {
            foreach (var i in _extraGet1Bill)
            {
                var tween = i.DOScale(1, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    i.DOScale(1.1f, 0.3f).From(1).SetLoops(-1, LoopType.Yoyo);
                });

                _tweenersExtra.Add(tween);
            }

            _get1Bil.gameObject.SetActive(false);
            return;
        }
        else if(_countClick1Bil > 3)
        {
            _tweener = _get5mPerMonth.DOScale(1.1f, 0.4f).From(1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            foreach (var i in _extraGet1Bill)
            {
                i.gameObject.SetActive(false);
            }

            foreach (var i in _tweenersExtra)
            {
                i?.Kill();
            }

            return;
        }
        
        int x = Random.Range(-150, 150);
        float y = Random.Range(-800, 800);
        _get1Bil.anchoredPosition = new Vector2(x, y);
    }

    public void Get5MPerMonth()
    {
        Sequence sq = DOTween.Sequence();
        _chungNhan.gameObject.SetActive(true);
        if (User.Sound)
            AudioManager.Instance.PlaySfx("tuyendung");
        sq.Append(_chungNhan.transform.DOScale(1, 0.3f).SetEase(Ease.Linear).From(4))
          .AppendInterval(0.5f)
          .AppendCallback(() =>
          {
              Manager.Load(TakePhotoController.TAKEPHOTO_SCENE_NAME);
          });
    }

    private void OnDestroy()
    {
        _tweener.Kill();
        foreach (var i in _tweenersExtra)
        {
            i?.Kill();
        }
    }
}