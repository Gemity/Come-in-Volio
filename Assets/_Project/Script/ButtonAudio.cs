using Lean.Common;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private string _sfx;
    private void Start()
    {
        var button = GetComponent<Button>();
        if (button != null) { button.onClick.AddListener(Play); }
    }

    public void Play()
    {
        if (AudioManager.Instance != null && User.Sound)
        {
            if (string.IsNullOrEmpty(_sfx))
                AudioManager.Instance.PlayButtonTapSfx();
            else
                AudioManager.Instance.PlaySfx(_sfx);
        }
    }
}