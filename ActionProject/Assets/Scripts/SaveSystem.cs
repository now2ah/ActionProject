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
    public string date; 
    public GameData gameData;
    public List<UnitData> unitDatas;
}

[System.Serializable]
public class WrapData
{
    public List<Data> dataSlots;
}

public class SaveSystem : Singleton<SaveSystem>
{
    List<Data> dataSlots;
    public List<Data> DataSlots => dataSlots;

    public override void Initialize()
    {
        base.Initialize();
        dataSlots = new List<Data>();
        Data emptyData = new Data();
        emptyData.date = "EMPTY";
        for(int i=0; i<5; i++)
            dataSlots.Add(emptyData);
    }

    public bool HasSaveData()
    {
        return dataSlots != null && "EMPTY" != dataSlots[0].date;
    }

    public bool HasSaveDataInSlot(int index)
    {
        return dataSlots[index] != null;
    }

    public Data Load(int slotNum)
    {
        //string dataPath = Path.Combine(Application.dataPath, "GameData" + slotNum);

        //if (!Directory.Exists(dataPath))
        //    Logger.Log("file is not exist.");

        //Data data = JsonParser.LoadJsonFile<Data>(dataPath, "GameData" + slotNum);
        
        return dataSlots[slotNum];
    }

    public void Save(int slotNum, Data data)
    {
        //string dataString = JsonParser.ObjectToJson(data);

        //string dataPath = Path.Combine(Application.dataPath, "GameData" + slotNum);
        //if (!Directory.Exists(dataPath))
        //    Directory.CreateDirectory(dataPath);

        //JsonParser.CreateJsonFile(dataPath, "GameData" + slotNum, dataString);

        dataSlots[slotNum] = data;
        SaveSourceData();
    }

    public void LoadSourceData()
    {
        string dataPath = Path.Combine(Application.dataPath, "SaveData");
        string dataFilePath = Path.Combine(dataPath, "SaveData.json");

        if (!Directory.Exists(dataPath) || !Directory.Exists(dataFilePath))    //data가 없으면 경우 추가
        {
            Directory.CreateDirectory(dataPath);
            WrapData emptyWrapData = new WrapData();
            emptyWrapData.dataSlots = new List<Data>();
            Data emptyData = new Data();
            emptyData.date = "EMPTY";
            for (int i = 0; i < 5; i++)
                emptyWrapData.dataSlots.Add(emptyData);
            string dataString = JsonParser.ObjectToJson(emptyWrapData);
            JsonParser.CreateJsonFile(dataPath, "SaveData", dataString);
        }

        WrapData data = JsonParser.LoadJsonFile<WrapData>(dataPath, "SaveData");
        dataSlots = data.dataSlots;
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
