using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Action.Manager;
using Action.Units;
using Action.Util;

[System.Serializable]
public class Data
{
    public GameData gameData;
    public List<UnitData> unitDatas;
}

[System.Serializable]
public class WrapData
{
    public Data[] dataSlots;
}

public class SaveSystem : Singleton<SaveSystem>
{
    public Data[] dataSlots;

    public override void Initialize()
    {
        base.Initialize();
        dataSlots = new Data[5];
    }

    public bool HasSaveData()
    {
        return dataSlots != null && dataSlots.Length > 0;
    }

    public bool HasSaveDataInSlot(int index)
    {
        return dataSlots[index] != null;
    }

    public Data Load(int slotNum)
    {
        string dataPath = Path.Combine(Application.dataPath, "GameData" + slotNum);

        if (!Directory.Exists(dataPath))
            Logger.Log("file is not exist.");

        Data data = JsonParser.LoadJsonFile<Data>(dataPath, "GameData" + slotNum);
        return data;
    }

    public void Save(int slotNum, Data data)
    {
        string dataString = JsonParser.ObjectToJson(data);

        string dataPath = Path.Combine(Application.dataPath, "GameData" + slotNum);
        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);

        JsonParser.CreateJsonFile(dataPath, "GameData" + slotNum, dataString);
        SaveSourceData();
    }

    public void LoadSourceData()
    {
        string dataPath = Path.Combine(Application.dataPath, "SaveData");

        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
            WrapData emptyData = new WrapData();
            string dataString = JsonParser.ObjectToJson(emptyData);
            JsonParser.CreateJsonFile(dataPath, "SaveData", dataString);
        }

        WrapData data = JsonParser.LoadJsonFile<WrapData>(dataPath, "SaveData");
        data.dataSlots = dataSlots;
    }

    public void SaveSourceData()
    {
        WrapData data = new WrapData();
        data.dataSlots = dataSlots;
        string dataString = JsonParser.ObjectToJson(data);

        string dataPath = Path.Combine(Application.dataPath, "SaveData");
        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);

        JsonParser.CreateJsonFile(dataPath, "SaveData", dataString);
    }
}
