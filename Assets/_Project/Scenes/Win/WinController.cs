using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using DG.Tweening;

public class WinController : Controller
{
    public const string WIN_SCENE_NAME = "Win";

    public override string SceneName()
    {
        return WIN_SCENE_NAME;
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMPro.TMP_Text _scoreText;

    private int _score;
    public override void OnActive(object data)
    {
        if (data != null)
            _score = (int)data;
    }

    private void Start()
    {
        _canvas.worldCamera = Manager.Object.UICamera;
        DOTween.To(() => 0, value => _scoreText.text = value.ToString(), _score, 0.5f);
    }
}