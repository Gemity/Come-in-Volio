using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnSetting
{
    public string prefabPath;
    public float offsetPosition;
    public float rateOverTime;
    public float delay;
}

[CreateAssetMenu(fileName = "StageData", menuName = "Gameplay/Stage Data")]
public class StageData : ScriptableObject
{
    [SerializeField] private SerializedDictionary<int, SpawnSetting> _spawnGroupSetting;

    public SerializedDictionary<int, SpawnSetting> SpawnGroupSetting => _spawnGroupSetting;
}
