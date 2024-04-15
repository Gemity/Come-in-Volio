using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct CharacterData
{
    public string name;
    public int id;
    public Sprite faceSprite;
    public Sprite bodySprite;
}

[CreateAssetMenu(fileName = "CharacterInGame", menuName = "Gameplay/CharacterInGame")]
public class CharacterDataSO : SingletonScriptableObject<CharacterDataSO>
{
    [SerializedDictionary(keyName:"Group id", valueName: "Data")][SerializeField]
    private SerializedDictionary<int, List<CharacterData>> _charactersData;

    public CharacterData GetCharacterData(int id)
    {
        return _charactersData.Values.SelectMany(list => list).FirstOrDefault(character => character.id == id);
    }

    public CharacterData GetCharacterData(string name)
    {
        return _charactersData.Values.SelectMany(list => list).FirstOrDefault(character => character.name.Equals(name));
    }

    public List<CharacterData> GetCharactersDataByGroupId(int groupId)
    {
        if (!_charactersData.ContainsKey(groupId))
            return null;
        return _charactersData[groupId];
    }
}
