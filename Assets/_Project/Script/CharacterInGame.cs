using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CharacterData
{
    public string name;
    public int id;
    public int groupId;
    public Sprite faceSprite;
    public Sprite bodySprite;
}

[CreateAssetMenu(fileName = "CharacterInGame", menuName = "Gameplay/CharacterInGame")]
public class CharacterInGame : SingletonScriptableObject<CharacterInGame>
{
    [SerializeField] private List<CharacterData> data;

    public List<CharacterData> Data => data;
    public CharacterData GetCharacterData(int id)
    {
        return data.Find(x => x.id == id);
    }

    public CharacterData GetCharacterData(string name)
    {
        return data.Find(x => x.name.Equals(name));
    }

    public List<CharacterData> GetCharactersDataByGroupId(int groupId)
    {
        return data.FindAll(x => x.groupId == groupId);
    }
}
