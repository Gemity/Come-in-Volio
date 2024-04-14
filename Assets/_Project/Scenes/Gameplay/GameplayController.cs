using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameplayController : Controller
{
    public const string GAMEPLAY_SCENE_NAME = "Gameplay";

    public override string SceneName()
    {
        return GAMEPLAY_SCENE_NAME;
    }

    [SerializeField] private SpawnCharacter _spawnCharacter;
    [SerializeField] private Player _player;

    private StageData _stageData;
    public override void OnActive(object data)
    {
        int stageId = 1;
        if(data != null)
            stageId = (int)data;

        _stageData = Resources.Load<StageData>($"StageData/Stage_{stageId}");
    }

    private void Start()
    {
        _spawnCharacter.Setup(_stageData);
        _player.Init();
        InputControl.Instance.EnableInput = true;
    }

    public void ResignCallBack4Object(CharacterObject objetc)
    {

    }
}