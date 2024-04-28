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
        DOTween.To(() => 0, value => _scoreText.text = value.ToString(), _score, 1);

        if (User.Sound)
            AudioManager.Instance.PlaySfx("Win-music");
    }

    public void OnNext()
    {
        Manager.Load(MapController.MAP_SCENE_NAME);
    }
}