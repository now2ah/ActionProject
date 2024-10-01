using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Action.Manager;
using Action.Units;
using Action.Util;

public class Data
{
    public static GameData gameData;
    public static List<UnitData> unitDatas;
}

public class SaveSystem
{
    public Data[] dataSlots;

    public void Initialize()
    {
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
    }
}
