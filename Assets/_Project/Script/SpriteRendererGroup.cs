using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererGroup : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _spriteRenderer;

    private void Start()
    {
        foreach (var spriteRenderer in _spriteRenderer)
        {
            spriteRenderer.color = Vector4.zero;
        }
    }

    public void Show()
    {
        DOTween.To(() => 0f, value =>
        {
            foreach(var sprite in _spriteRenderer)
                sprite.color = new Color(1f, 1f, 1f, value);
        }, 1f, 1);
    }

    private void Reset()
    {
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
    }
}
