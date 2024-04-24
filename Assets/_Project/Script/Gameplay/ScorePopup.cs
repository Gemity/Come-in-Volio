using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Color _green, _red;
    [SerializeField] private float _delayDespawn;

    public void Setup(int score)
    {
        _scoreText.text = score > 0 ? $"+{score}" : score.ToString();
        _scoreText.color = score > 0 ? _green : _red;
        StartCoroutine(Co_Wait2Despawn());
    }

    private IEnumerator Co_Wait2Despawn()
    {
        yield return new WaitForSeconds(_delayDespawn);
        LeanPool.Despawn(this);
    }
}
