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

    private float _timer = 0;
    private StageData _stageData;
    private Coroutine _spawnRoutine;


    public void Setup(StageData stageData)
    {
        Random.InitState(Time.time.GetHashCode());
        _stageData = stageData;
        _spawnRoutine = StartCoroutine(Co_SpawnCharacter());
    }

    private IEnumerator Co_SpawnCharacter()
    {
        float maxTime = _stageData.GetMaxTimeline() + 1;
        List<float> timeline = _stageData.SpawnGroupSetting.Keys.ToList();

        Dictionary<int, List<CharacterData>> totalChartacter = _stageData.SpawnGroupSetting.Values.GroupBy(x => x.characterGroupId)
                                                               .ToDictionary(x => x.Key, y => y.Count())
                                                               .ToDictionary(x => x.Key, y => CharacterDataSO.Instance.GetCharactersDataByGroupId(y.Key, y.Value));

        while(_timer < maxTime)
        {
            for(int i=0; i< timeline.Count; i++)
            {
                if (timeline[i] < _timer)
                {
                    CharacterOnTimeline setting = _stageData.SpawnGroupSetting[timeline[i]];
                    CharacterData data;

                    if (!totalChartacter.ContainsKey(setting.characterGroupId) || totalChartacter[setting.characterGroupId].Count == 0)
                        continue;
                    else
                    {
                        data = totalChartacter[setting.characterGroupId][0];
                        totalChartacter[setting.characterGroupId].RemoveAt(0);
                    }

                    SpawnGroupSetting spawnGroup = SpawnGroupSettingSO.Instance.GetSpawnSettingById(setting.spawnGroupId);
                    CharacterGroupSetting characterGroup = CharacterGroupSettingSO.Instance.GetCharacterGroupById(setting.characterGroupId);
                    CharacterObject obj = LeanPool.Spawn<CharacterObject>(characterGroup.prefab);

                    if(spawnGroup.rayId > 0)
                    {
                        float x = (spawnGroup.rayId - 2) * Const.Distance_Each_Ray;
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

                    obj.name = data.name;
                    obj.Setup(data, characterGroup);

                    timeline.RemoveAt(i);
                    i--;
                }
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
