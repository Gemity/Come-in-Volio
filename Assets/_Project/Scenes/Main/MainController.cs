using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using UnityEngine.UI;
using DG.Tweening;

public class MainController : Controller
{
    public const string MAIN_SCENE_NAME = "Main";

    public override string SceneName()
    {
        return MAIN_SCENE_NAME;
    }

    [SerializeField] private Image _loading;
    private IEnumerator Start()
    {
        yield return _loading.DOFillAmount(1,3).WaitForCompletion();
        Manager.Load(WantedController.WANTED_SCENE_NAME);
    }
}