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
        public bool sound;
        public void Init()
        {
            stageId = 1;
            sound = true;
        }
    }

    static User()
    {
        Load();
    }

    private static UserData data;
    public static int StageId { get { return data.stageId; } set { data.stageId = value; } }
    public static bool Sound { get { return data.sound;} }
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
