using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using TMPro;
using Lean.Pool;
using UnityEngine.TextCore.Text;

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

    [SerializeField] private int _debugStageId;

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
        if (_debugStageId > 0)
            stageId = _debugStageId;
        else if (data != null)
            stageId = (int)data;

        _stageData = Resources.Load<StageData>($"Stage_{stageId}");
    }

    private void Start()
    {
        _scoreText.text = $"{_score}/{_stageData.ScoreRequire}";
        _spawnCharacter.Setup(_stageData);
        _player.Init();
        InputControl.Instance.EnableFinger = true;
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

    public void UpdateScore(int score, Vector3 popupPos)
    {
        _score += score;
        _scoreText.text = $"{_score}/{_stageData.ScoreRequire}";

        var popupScore = LeanPool.Spawn<ScorePopup>(_scorePopup, popupPos, Quaternion.identity);
        popupScore.Setup(score);

        if (_score >= _stageData.ScoreRequire)
            OnWin();
    }

    public void CheckLoseGame()
    {
        if(_score < _stageData.ScoreRequire)
        {
            OnLose();
        }
    }

    private void OnLose()
    {
        InputControl.Instance.EnableFinger = false;
        Manager.Add(LoseController.LOSE_SCENE_NAME);
    }

    private void OnWin()
    {
        InputControl.Instance.EnableFinger = false;

        if (User.StageId < Const.MaxStage)
        {
            User.StageId++;
            Manager.Add(WinController.WIN_SCENE_NAME, _score);
        }
        else
            CompleteGame();
    }

    private void CompleteGame()
    {
        _spawnCharacter.Stop();
        Manager.Add(CompleteGameController.COMPLETEGAME_SCENE_NAME);
    }
}