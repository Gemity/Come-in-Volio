using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GroupSetting
{
    public string name;
    public int groupId;
    public int health;
    public Vector2 speedX;
    public Vector2 speedY;
    public int score;
}

[CreateAssetMenu(fileName = "CharacterGroupSetting", menuName = "Gameplay/CharacterGroupSetting")]
public class GroupSettingSO : SingletonScriptableObject<GroupSettingSO>
{
    [SerializeField] private List<GroupSetting> _groupSettings;

    public GroupSetting GetGroupById(int groupId)
    {
        return _groupSettings.Find(x => x.groupId == groupId);
    }
}
