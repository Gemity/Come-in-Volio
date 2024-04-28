using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using DG.Tweening;
using Unity.VisualScripting;

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

    private int _countClick1Bil = 0;
    private IEnumerator Start()
    {
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
        yield return _canvasGroup.DOFade(0,0.4f).WaitForCompletion();
        yield return _reciveReward.DOAnchorPosY(0, 0.4f).SetEase(Ease.OutBack);
        _get1Bil.DOScale(1, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _get1Bil.DOScale(1.1f, 0.3f).From(1).SetLoops(-1, LoopType.Yoyo);
        });
        _get5mPerMonth.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    public void Get1Bil()
    {
        _countClick1Bil++;
        if (_countClick1Bil > 2)
            _get1Bil.gameObject.SetActive(false);
        int x = Random.Range(-150, 150);
        float y = Random.Range(-800, 800);
        _get1Bil.anchoredPosition = new Vector2(x, y);
    }

    public void Get5MPerMonth()
    {

    }
}