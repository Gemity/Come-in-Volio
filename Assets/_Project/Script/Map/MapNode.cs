using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    [SerializeField] private GameObject _lockGo, _currentGo, _passGo;
    [SerializeField] private int _stageId;

    private void Start()
    {
        _lockGo.SetActive(User.StageId < _stageId);
        _currentGo.SetActive(User.StageId == _stageId);
        _passGo.SetActive(User.StageId > _stageId);
    }
}
