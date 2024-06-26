using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CharacterGroupSetting
{
    public string name;
    public int groupId;
    public int health;
    public int score;

    public float speedX;
    public float speedY;

    public CharacterObject prefab;
}

[CreateAssetMenu(fileName = "CharacterGroupSettingSO", menuName = "Gameplay/CharacterGroupSettingSO")]
public class CharacterGroupSettingSO : SingletonScriptableObject<CharacterGroupSettingSO>
{
    [SerializeField] private List<CharacterGroupSetting> _characterGroupSetting;

    public CharacterGroupSetting GetCharacterGroupById(int groupId)
    {
        return _characterGroupSetting.Find(x => x.groupId == groupId);
    }
}