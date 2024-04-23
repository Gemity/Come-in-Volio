using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnGroupSetting
{
    public string name;
    public int id;
    public float rayId;
    public Vector2 boundCenter;
    public Vector2 boundSize;
}

[CreateAssetMenu(fileName = "SpawnGroupSettingSO", menuName = "Gameplay/SpawnGroupSettingSO")]
public class SpawnGroupSettingSO : SingletonScriptableObject<SpawnGroupSettingSO>
{
    [SerializeField] private List<SpawnGroupSetting> _spawnGroupSettings;

    public List<SpawnGroupSetting> SpawnGroupSettings => _spawnGroupSettings;
    public SpawnGroupSetting GetSpawnSettingById(int id)
    {
        return _spawnGroupSettings.Find(x => x.id == id);
    }
}
