using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using TMPro;
using Lean.Pool;
using UnityEngine.TextCore.Text;
using DG.Tweening;
using UnityEngine.UI;
using System.Security.Claims;

public class GameplayController : Controller
{
    private enum GameState
    {
        Notset = 0, Waiting = 1, Playing = 2, Win = 3, Lose = 4, Pause = 5
    }

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
    [SerializeField] private GameObject _tutorialContainer;
    [SerializeField] private RectTransform _tutorialPanel;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private AudioClip[] _soundsHitBullet;
    [SerializeField] private Image _notiDanger;
    [SerializeField] private TMP_Text _targetScore;
    [SerializeField] private GameObject _splash;

    [SerializeField] private int _debugStageId;

    private static GameplayController _instance;
    public static GameplayController Instance => _instance;

    private StageData _stageData;
    private int _score = 0;
    private Coroutine _playFxRoutine;
    private GameState _state;
    private bool _dangerBoss =false;

    private int _countGroup = 0;
    private void Awake()
    {
        _instance = this;    
    }

    public override void OnActive(object data)
    {
        int stageId = 1;
        if (_debugStageId > 0)
        {
            stageId = _debugStageId;
            User.StageId = stageId;
        }
        else
            stageId = User.StageId;

        _stageData = Resources.Load<StageData>($"Stage_{stageId}");
    }

    private void Start()
    {
        _state = GameState.Waiting;

        _scoreText.text = $"{_score}/{_stageData.ScoreRequire}";
        if (!User.ShowTutorial)
        {
            _tutorialContainer.SetActive(true);
            if (User.Sound)
                AudioManager.Instance.PlaySfx("Pop-up slide");
            _tutorialPanel.DOAnchorPosY(108, 0.5f).SetEase(Ease.OutBack);
            _tutorialButton.onClick.AddListener(() =>
            {
                if (User.Sound)
                    AudioManager.Instance.PlaySfx("Pop-up slide");
                _tutorialPanel.DOAnchorPosY(1768, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    _tutorialContainer.SetActive(false);
                    PlayGame();
                    User.ShowTutorial = true;
                });
            });
        }
        else
        {
            PlayGame();
        }
    }

    private void Update()
    {
        if(User.StageId == 5 && !_dangerBoss && Time.timeSinceLevelLoad > 35 && _state == GameState.Playing)
        {
            _dangerBoss = true;
            ShowDanger();
        }
    }

    private void ShowDanger()
    {
        if (User.Sound)
            AudioManager.Instance.PlaySfx("danger", 1, false, true);
        _notiDanger.gameObject.SetActive(true);
        _notiDanger.DOFade(1, 1).From(0).SetLoops(6, LoopType.Yoyo).OnComplete(() =>
        {
            AudioManager.Instance.StopSfx();
            _notiDanger.gameObject.SetActive(false);
        });

    }

    public void PlayGame()
    {
        _state = GameState.Playing;
        _spawnCharacter.Setup(_stageData);
        _player.Init();
        InputControl.Instance.EnableFinger = true;
        if (User.Sound)
            AudioManager.Instance.PlayBgm("BGM");

        _playFxRoutine = StartCoroutine(Co_Playfx());
        StartCoroutine(Co_DisplayTargetScore());
    }
    
    public IEnumerator Co_Splash()
    {
        _splash.SetActive(true );
        yield return new WaitForSeconds(0.1f);
        _splash.SetActive(false);
    }

    private IEnumerator Co_DisplayTargetScore()
    {
        _targetScore.gameObject.SetActive(true);
        _targetScore.text = $"Target score : {_stageData.ScoreRequire}";

        yield return DOTween.To(() => 0f, value =>
        {
            _targetScore.color = new Color(1, 1, 1, value);
        }, 1, 0.5f).WaitForCompletion();

        yield return new WaitForSeconds(2);

        yield return DOTween.To(() => 1f, value =>
        {
            _targetScore.color = new Color(1, 1, 1, value);
        }, 0, 0.5f).WaitForCompletion();

        _targetScore.gameObject.SetActive(false);
    }

    private IEnumerator Co_Playfx()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 7f));
            if (User.Sound)
                AudioManager.Instance.PlaySfx("Brr00");
        }
    }


    public void ToggleSound()
    {
        User.Sound = !User.Sound;
        _soundOff.SetActive(!User.Sound);
        _soundOn.SetActive(User.Sound);

        AudioManager.Instance.pauseBgm = !User.Sound;
    }

    public void UpdateScore(CharacterObject character)
    {
        _score += character.Score;
        if(_score < 0)
            _score = 0;

        _scoreText.text = $"{_score}/{_stageData.ScoreRequire}";

        var popupScore = LeanPool.Spawn<ScorePopup>(_scorePopup, character.transform.position, Quaternion.identity);
        popupScore.Setup(character.Score);

        if(User.Sound && character.Score > 0 && character.Type != CharacterType.Group)
        {
            var cliip = _soundsHitBullet[Random.Range(0, _soundsHitBullet.Length)];
            AudioManager.Instance.PlaySfx(cliip);
        }

        if(User.StageId == 3 && character.Id == 2)
        {
            AudioManager.Instance.PlaySfx("yeah02");
        }
        else if(User.StageId == 4 && (character.Id == 8 || character.Id == 9))
        {
            _countGroup++;
            if(_countGroup > 1)
            {
                AudioManager.Instance.PlaySfx("yeah02");
                _countGroup = 0;
            }
        }
    }

    public void UpdateScore(int score, Vector3 popupPos)
    {
        _score += score;
        _scoreText.text = $"{_score}/{_stageData.ScoreRequire}";

        var popupScore = LeanPool.Spawn<ScorePopup>(_scorePopup, popupPos, Quaternion.identity);
        popupScore.Setup(score);
    }

    public void CheckGameState()
    {
        if(_score < _stageData.ScoreRequire)
        {
            OnLose();
        }
        else
            OnWin();
    }

    private void OnLose()
    {
        if (_state == GameState.Lose)
            return;

        _state = GameState.Lose;
        InputControl.Instance.EnableFinger = false;
        Manager.Add(LoseController.LOSE_SCENE_NAME);
    }

    private void OnWin()
    {
        if(_state == GameState.Win) return;

        _state = GameState.Win;
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

    private void OnDestroy()
    {
        if (_playFxRoutine != null)
            StopCoroutine(_playFxRoutine);
    }
}