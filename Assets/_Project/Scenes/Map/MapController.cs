using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class MapController : Controller
{
    public const string MAP_SCENE_NAME = "Map";

    public override string SceneName()
    {
        return MAP_SCENE_NAME;
    }

    public void EnterStage()
    {
        Manager.Load(GameplayController.GAMEPLAY_SCENE_NAME);
    }
}