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
}

[System.Serializable]
public class WrapData
{
    public List<Data> dataSlots;
}

public class SaveSystem : Singleton<SaveSystem>
{
    List<Data> saveDataSlots;
    public List<Data> DataSlots => saveDataSlots;

    public override void Initialize()
    {
        base.Initialize();
        saveDataSlots = new List<Data>();
        Data emptyData = new Data();
        emptyData.date = "EMPTY";
        for(int i=0; i<5; i++)
            saveDataSlots.Add(emptyData);
    }

    public bool HasSaveData()
    {
        return saveDataSlots != null && "EMPTY" != saveDataSlots[0].date;
    }

    public bool HasSaveDataInSlot(int index)
    {
        return saveDataSlots[index] != null;
    }

    public void Load(int slotNum)
    {
        GameManager.Instance.ResetGame();
        Data data = saveDataSlots[slotNum];
        GameManager.Instance.GameData = data.gameData;
        SceneManager.Instance.LoadGameScene(2);
    }

    public void Save(int slotNum, Data data)
    {
        saveDataSlots[slotNum] = data;
        SaveSourceData();
    }

    public void LoadSourceData()
    {
        string dataPath = Path.Combine(Application.dataPath, "SaveData");
        string dataFilePath = Path.Combine(dataPath, "SaveData.json");

        if (!Directory.Exists(dataPath) || !File.Exists(dataFilePath))    //data가 없으면 경우 추가
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
        saveDataSlots = data.dataSlots;
    }

    public void SaveSourceData()
    {
        WrapData data = new WrapData();
        data.dataSlots = saveDataSlots;
        string dataString = JsonParser.ObjectToJson(data);

        string dataPath = Path.Combine(Application.dataPath, "SaveData");
        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);

        JsonParser.CreateJsonFile(dataPath, "SaveData", dataString);
    }
}
