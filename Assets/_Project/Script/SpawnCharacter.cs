using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using System;
using Random = UnityEngine.Random;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] private float _spawnPosY;

    private Dictionary<int, Coroutine> _spawnGroupRoutine;
    private Dictionary<string, CharacterObject> _prefabs;

    public void Setup(StageData stageData)
    {
        Random.InitState(DateTime.Now.GetHashCode());
        _spawnGroupRoutine = new Dictionary<int, Coroutine>();
        _prefabs = new Dictionary<string, CharacterObject>();

        foreach(var spawnGroup in stageData.SpawnGroupSetting)
        {
            var routine = StartCoroutine(Co_SpawnCharacter(spawnGroup.Key, spawnGroup.Value));
            _spawnGroupRoutine.Add(spawnGroup.Key, routine);
        }
    }

    private IEnumerator Co_SpawnCharacter(int groupId, SpawnSetting spawnSetting)
    {
        List<CharacterData> characters = CharacterInGame.Instance.GetCharactersDataByGroupId(groupId);

        if(!_prefabs.ContainsKey(spawnSetting.prefabPath))
        {
            var obj = Resources.Load<CharacterObject>(spawnSetting.prefabPath);
            _prefabs.Add(spawnSetting.prefabPath, obj);
        }

        var prefab = _prefabs[spawnSetting.prefabPath];

        yield return new WaitForSeconds(spawnSetting.delay);
        while (true)
        {
            CharacterData data = characters.GetRandomElement();
            Vector3 pos = new Vector3(Random.Range(-spawnSetting.offsetPosition, spawnSetting.offsetPosition), _spawnPosY, 0);
            CharacterObject obj = LeanPool.Spawn<CharacterObject>(prefab, pos, Quaternion.identity);
            obj.Setup(data, groupId);

            yield return new WaitForSeconds(spawnSetting.rateOverTime);
        }
    }

    private void Release()
    {
        foreach(var i in  _spawnGroupRoutine)
        {
            StopCoroutine(i.Value);
        }
    }
}
