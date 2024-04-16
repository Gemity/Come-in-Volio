using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Dictionary<float, CharacterOnTimeline> _spawnGroupSetting = new Dictionary<float, CharacterOnTimeline>
        {
            { 0.5f, new CharacterOnTimeline { spawnGroupId = 1, characterGroupId = 10 } },
            { 1.0f, new CharacterOnTimeline { spawnGroupId = 2, characterGroupId = 10 } },
            { 1.5f, new CharacterOnTimeline { spawnGroupId = 1, characterGroupId = 30 } },
            { 2.0f, new CharacterOnTimeline { spawnGroupId = 3, characterGroupId = 20 } }
        };

        Dictionary<int, int> resultDictionary = _spawnGroupSetting.Values
            .GroupBy(pair => pair.characterGroupId) 
            .ToDictionary(group => group.Key, group => group.Count()); 

        foreach (var pair in resultDictionary)
        {
            Debug.Log("id: " + pair.Key + ", sum : " + pair.Value);
        }
    }
}
