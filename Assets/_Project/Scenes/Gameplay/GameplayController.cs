using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using TMPro;
using Lean.Pool;

public class GameplayController : Controller
{
    public const string GAMEPLAY_SCENE_NAME = "Gameplay";

    public override string SceneName()
    {
        return GAMEPLAY_SCENE_NAME;
    }

    [SerializeField] private SpawnCharacter _spawnCharacter;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _soundOn, _soundOff;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private ScorePopup _scorePopup;

    private static GameplayController _instance;
    public static GameplayController Instance => _instance;

    private StageData _stageData;
    private int _score = 0;

    private void Awake()
    {
        _instance = this;    
    }

    public override void OnActive(object data)
    {
        int stageId = 1;
        if(data != null)
            stageId = (int)data;

        _stageData = Resources.Load<StageData>($"StageData/Stage_{stageId}");
    }

    private void Start()
    {
        _scoreText.text = $"{_score}/{_stageData.ScoreRequire}";
        _spawnCharacter.Setup(_stageData);
        _player.Init();
        InputControl.Instance.EnableInput = true;
    }

    public void ResignCallBack4Object(CharacterObject objetc)
    {

    }

    public void ToggleSound()
    {
        User.Sound = !User.Sound;
        _soundOff.SetActive(!User.Sound);
        _soundOn.SetActive(User.Sound);
    }

    public void UpdateScore(CharacterObject character)
    {
        _score += character.Score;
        _scoreText.text = $"{_score}/{_stageData.ScoreRequire}";

        var popupScore = LeanPool.Spawn<ScorePopup>(_scorePopup, character.transform.position, Quaternion.identity);
        popupScore.Setup(character.Score);

        if (_score >= _stageData.ScoreRequire)
            OnWin();
    }

    private void OnWin()
    {

    }
}