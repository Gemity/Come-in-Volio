using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class LoseController : Controller
{
    public const string LOSE_SCENE_NAME = "Lose";

    public override string SceneName()
    {
        return LOSE_SCENE_NAME;
    }

    public void PlayAgain()
    {
        Manager.Load(GameplayController.GAMEPLAY_SCENE_NAME);
    }
}