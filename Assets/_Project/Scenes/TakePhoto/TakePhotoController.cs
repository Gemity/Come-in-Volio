using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using UnityEngine.UI;
using DG.Tweening;

public class TakePhotoController : Controller
{
    public const string TAKEPHOTO_SCENE_NAME = "TakePhoto";

    public override string SceneName()
    {
        return TAKEPHOTO_SCENE_NAME;
    }

    [SerializeField] private Image _shield;
    [SerializeField] private Image _splash;
    [SerializeField] private SpriteRendererGroup _spriteRendererGroup;
    [SerializeField] private GameObject _effAppearArmy;
    [SerializeField] private Animation _captureAnim;
    [SerializeField] private GameObject _captureGo;
    [SerializeField] private RectTransform[] _albumElement;
    [SerializeField] private GameObject _album;
    [SerializeField] private Image _happyBirthday;
    [SerializeField] private CanvasGroup _canvasAlbum;
    [SerializeField] private GameObject _endGame;

    private IEnumerator Start()
    {
        yield return _shield.DOFade(0, 1).SetEase(Ease.Linear).WaitForCompletion();
        yield return new WaitForSeconds(0.2f);
        _effAppearArmy.SetActive(true);
        yield return new WaitForSeconds(1);
        _spriteRendererGroup.Show();
        yield return new WaitForSeconds(2.5f);
        _effAppearArmy.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _captureGo.SetActive(true);
        _captureAnim.Play();
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => FingerDownThisFrame());
        _splash.gameObject.SetActive(true);
        yield return _shield.DOFade(0, 0.25f).WaitForCompletion();
        _splash.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _captureGo.SetActive(false);
        _album.SetActive(true);
        _shield.color = new Color(0, 0, 0, 200f / 255f);
        for (int i = 0; i < _albumElement.Length; i++)
        {
            yield return _albumElement[i].DOMoveY(0, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
            if (i > 0)
                _albumElement[i - 1].gameObject.SetActive(false);
            if (i == _albumElement.Length - 1)
                _happyBirthday.DOFade(1, 0.5f);
            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => FingerDownThisFrame());
        }

        yield return _canvasAlbum.DOFade(0, 0.5f).WaitForCompletion();
        yield return new WaitForSeconds(1);
        _endGame.SetActive(true);
    }

    private bool FingerDownThisFrame()
    {
        if (Input.touchSupported)
            return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
        else
            return Input.GetMouseButtonDown(0);
    }
}