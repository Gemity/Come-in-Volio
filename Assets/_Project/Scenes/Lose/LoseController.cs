using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using UnityEngine.UI;
using DG.Tweening;

public class LoseController : Controller
{
    public const string LOSE_SCENE_NAME = "Lose";

    public override string SceneName()
    {
        return LOSE_SCENE_NAME;
    }

    private void Start()
    {
        if (User.Sound)
            AudioManager.Instance.PlaySfx("Lose-sound");
    }

    public void PlayAgain()
    {
        Manager.Load(GameplayController.GAMEPLAY_SCENE_NAME);
    }
}