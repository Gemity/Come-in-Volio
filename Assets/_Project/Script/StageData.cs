using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct CharacterOnTimeline
{
    public int spawnGroupId;
    public int characterGroupId;
}

[CreateAssetMenu(fileName = "StageData", menuName = "Gameplay/Stage Data")]
public class StageData : ScriptableObject
{
    [SerializeField] private int _scoreRequire;

    [SerializedDictionary(keyName:"Timeline", valueName:"SpawnSetting")][SerializeField] private SerializedDictionary<float, CharacterOnTimeline> _spawnGroupSetting;

    public SerializedDictionary<float, CharacterOnTimeline> SpawnGroupSetting => _spawnGroupSetting;
    public int ScoreRequire => _scoreRequire;

    public Dictionary<float, CharacterOnTimeline> GetSpawnSettingCopy()
    {
        Dictionary<float,CharacterOnTimeline> clone = new Dictionary<float,CharacterOnTimeline>();
        foreach(var kvp in _spawnGroupSetting)
        {
            clone.Add(kvp.Key, kvp.Value);
        }

        return clone;
    }

    public float GetMaxTimeline()
    {
        return _spawnGroupSetting.Keys.Max();
    }
}
