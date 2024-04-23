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
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Gameplay/CharacterDataSO")]
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

    public List<CharacterData> GetCharactersDataByGroupId(int groupId, int size)
    {
        if (!_charactersData.ContainsKey(groupId))
            return null;

        List<CharacterData> temp = new List<CharacterData>(_charactersData[groupId]);
        temp.Shuffle();
        if (size <= temp.Count)
            return temp.GetRange(0, size);
        else
        {
            int x = size / temp.Count;
            int mob = size % temp.Count;
            var result = temp.SelectMany(character => Enumerable.Repeat(character, x)).ToList();
            result.AddRange(temp.GetRange(0, mob));

            return result;
        }
    }
}
