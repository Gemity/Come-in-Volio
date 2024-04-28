using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class WantedController : Controller
{
    public const string WANTED_SCENE_NAME = "Wanted";

    public override string SceneName()
    {
        return WANTED_SCENE_NAME;
    }

    [SerializeField] private TMP_Text _requestText;
    [SerializeField] private TMP_Text _getKeoText;
    [SerializeField] private Transform _requestTf, _getKeoTf;

    [SerializeField] private string _request;
    [SerializeField] private string _get;

    [SerializeField] private Transform _nextTf;
    [SerializeField] private Transform _poster;

    private void Start()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(_poster.DOScale(1, 0.4f).From(2.5f))
          .AppendInterval(0.2f)
          .Append(_requestTf.DOScale(1, 0.4f).From(0).SetEase(Ease.OutBack))
          .AppendCallback(() =>
          {
              if (User.Sound)
                  AudioManager.Instance.PlaySfx("Type text");
          })
          .Append(DOTween.To(() => string.Empty, value => _requestText.text = value, _request, 2f))
          .AppendCallback(() => AudioManager.Instance.StopSfx())
          .AppendInterval(0.5f)
          .Append(_getKeoTf.DOScale(1, 0.4f).From(0).SetEase(Ease.OutBack))
          .AppendCallback(() =>
          {
              if (User.Sound)
                  AudioManager.Instance.PlaySfx("Type text");
          })
          .Append(DOTween.To(() => string.Empty, value => _getKeoText.text = value, _get, 1f))
          .AppendCallback(() => AudioManager.Instance.StopSfx())
          .AppendInterval(0.5f)
          .Append(_nextTf.DOScale(1, 0.4f).From(0).SetEase(Ease.OutBack));
    }



    public void Next()
    {
        Manager.Load(GameplayController.GAMEPLAY_SCENE_NAME);
    }
}