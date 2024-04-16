using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Random = UnityEngine.Random;
using System.Linq;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] private float _spawnPosY;
    [SerializeField] private float _distanceEachRay = 3f; 

    private Dictionary<string, CharacterObject> _prefabs;
    private float _timer = 0;
    private StageData _stageData;
    private Coroutine _spawnRoutine;


    public void Setup(StageData stageData)
    {
        Random.InitState(Time.time.GetHashCode());
        _stageData = stageData;
        _prefabs = new Dictionary<string, CharacterObject>();

        _spawnRoutine = StartCoroutine(Co_SpawnCharacter());
    }

    private IEnumerator Co_SpawnCharacter()
    {
        float maxTime = _stageData.GetMaxTimeline();
        List<float> keys = _stageData.SpawnGroupSetting.Keys.ToList();

        while(_timer < maxTime)
        {
            for(int i=0; i<keys.Count; i++)
            {
                if (keys[i] < _timer)
                {
                    CharacterOnTimeline setting = _stageData.SpawnGroupSetting[keys[i]];
                    CharacterGroupSetting characterGroup = CharacterGroupSettingSO.Instance.GetCharacterGroupById(setting.characterGroupId);
                    SpawnGroupSetting spawnGroup = SpawnGroupSettingSO.Instance.GetSpawnSettingById(setting.spawnGroupId);

                    CharacterObject obj = LeanPool.Spawn<CharacterObject>(characterGroup.prefabPath);

                    if(spawnGroup.rayId > 0)
                    {
                        float x = (spawnGroup.rayId - 2) * _distanceEachRay;
                        Vector3 pos = new Vector3(x, _spawnPosY, 0);
                        obj.transform.position = pos;
                    }
                    else
                    {
                        float x = Random.Range(spawnGroup.boundCenter.x - spawnGroup.boundSize.x, spawnGroup.boundCenter.x + spawnGroup.boundSize.x);
                        float y = Random.Range(spawnGroup.boundCenter.y - spawnGroup.boundSize.y, spawnGroup.boundCenter.y + spawnGroup.boundSize.y);

                        Vector3 pos = new Vector3(x, y, 0);
                        obj.transform.position = pos;
                        obj.PlayAppearFx();
                    }
                }

                keys.RemoveAt(i);
                i--;
            }

            yield return null;
            _timer += Time.deltaTime;
        }
    }

    private void Release()
    {
        StopCoroutine(_spawnRoutine);
    }
}
