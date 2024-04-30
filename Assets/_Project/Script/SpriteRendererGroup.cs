using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererGroup : MonoBehaviour
{
    [System.Serializable]
    public struct Character
    {
        public List<SpriteRenderer> renderers;
    }

    [SerializeField] private List<Character> _spriteRenderer;
    [SerializeField] private float _delay = 0.4f;

    private void Start()
    {
        foreach (var spriteRenderer in _spriteRenderer)
        {
            foreach(var i in spriteRenderer.renderers)
                i.color = Vector4.zero;
        }
    }

    public void Show()
    {
        for(int i =0;i< _spriteRenderer.Count; i++)
        {
            int cache = i;
            DOTween.To(() => 0f, value =>
            {
                foreach (var sprite in _spriteRenderer[cache].renderers)
                    sprite.color = new Color(1f, 1f, 1f, value);
            }, 1f, 1).SetDelay(cache * _delay);
        }
    }
}
