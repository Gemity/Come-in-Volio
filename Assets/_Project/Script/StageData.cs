using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct SpawnSetting
{
    public int spawnGroupId;
    public int characterGroupId;
}

[CreateAssetMenu(fileName = "StageData", menuName = "Gameplay/Stage Data")]
public class StageData : ScriptableObject
{
    [SerializedDictionary(keyName:"Timeline", valueName:"SpawnSetting")][SerializeField] private SerializedDictionary<float, SpawnSetting> _spawnGroupSetting;

    public SerializedDictionary<float, SpawnSetting> SpawnGroupSetting => _spawnGroupSetting;

    public Dictionary<float, SpawnSetting> GetSpawnSettingCopy()
    {
        Dictionary<float,SpawnSetting> clone = new Dictionary<float,SpawnSetting>();
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
