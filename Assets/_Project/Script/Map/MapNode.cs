using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    private const string Blend_Value = "_BlendValue";

    [SerializeField] private GameObject _lockGo, _currentGo, _passGo;
    [SerializeField] private int _stageId;
    [SerializeField] private Image _road;
    [SerializeField] private Image _pass, _current;
    [SerializeField] private Animation _halo;
    [SerializeField] private Animation _flagAnim;
    [SerializeField] private GameObject _infoCompany;

    private IEnumerator Start()
    {
        int BlendShaderProperty = Shader.PropertyToID(Blend_Value);

        _lockGo.SetActive(User.StageId < _stageId);
        _currentGo.SetActive(User.StageId == _stageId);
        _passGo.SetActive(User.StageId > _stageId);
        _infoCompany.SetActive(User.StageId >= _stageId);
        _road.fillAmount = User.StageId - _stageId > 1? 1 : 0;


        if (User.StageId == 1 && _stageId == 1)
        {
            DOTween.To(() => 1f, value => _current.material.SetFloat(BlendShaderProperty, value), 0f, 0.4f).SetEase(Ease.Linear);
            if (_halo != null)
            {
                _halo.Play();
            }
        }
        else if (_stageId == User.StageId - 1)
        {
            DOTween.To(() => 1f, value => _pass.material.SetFloat(BlendShaderProperty, value), 0f, 0.5f).SetEase(Ease.Linear);
            _flagAnim.Play();
            yield return new WaitForSeconds(0.5f);
            yield return _road.DOFillAmount(1, 1.5f).SetEase(Ease.Linear).WaitForCompletion();
        }
        else if(_stageId == User.StageId)
        {
            _current.material.SetFloat(BlendShaderProperty, 1);
            yield return new WaitForSeconds(2f);
            DOTween.To(() => 1f, value => _current.material.SetFloat(BlendShaderProperty, value), 0f, 0.4f).SetEase(Ease.Linear);
            if(_halo != null)
            {
                _halo.Play();
            }
        }

    }
}
