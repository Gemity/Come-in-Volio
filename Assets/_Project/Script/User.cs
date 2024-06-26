using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public static class User
{
    const string KeySave = "User_data";
    class UserData
    {
        public int stageId;
        public void Init()
        {
            stageId = 1;
        }
    }

    static User()
    {
        Load();
    }

    private static UserData data;
    public static int StageId { get { return data.stageId; } set { data.stageId = value; } }

    private static void Load()
    {
        string json = PlayerPrefs.GetString(KeySave, string.Empty);
        if(!string.IsNullOrEmpty(json))
        {
          data = JsonMapper.ToObject<UserData>(json);
        }
        else
        {
            data = new UserData();
            data.Init();
        }
    }

    public static void Save()
    {
        string json = JsonMapper.ToJson(data);
        PlayerPrefs.SetString(KeySave, json);
    }
}
