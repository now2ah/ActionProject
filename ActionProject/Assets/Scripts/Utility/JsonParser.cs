using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Action.Util
{
    public static class JsonParser
    {
        public static string ObjectToJson(object obj) { return JsonUtility.ToJson(obj); }
        public static T JsonToOject<T>(string jsonData) { return JsonUtility.FromJson<T>(jsonData); }

        public static void CreateJsonFile(string createPath, string fileName, string jsonData) 
        { 
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create); 
            byte[] data = Encoding.UTF8.GetBytes(jsonData); fileStream.Write(data, 0, data.Length); 
            fileStream.Close(); 
        }

        public static T LoadJsonFile<T>(string loadPath, string fileName) 
        { 
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open); 
            byte[] data = new byte[fileStream.Length]; fileStream.Read(data, 0, data.Length); 
            fileStream.Close(); 
            string jsonData = Encoding.UTF8.GetString(data); return JsonUtility.FromJson<T>(jsonData); 
        }
    }
}

