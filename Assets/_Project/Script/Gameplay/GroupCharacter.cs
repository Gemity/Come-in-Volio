using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroupCharacter : OneSpriteCharacter
{
    [SerializeField] private TMP_Text _textDisplay;

    public override void Setup(CharacterData characterData, CharacterGroupSetting characterGroup)
    {
        base.Setup(characterData, characterGroup);
    }
}
