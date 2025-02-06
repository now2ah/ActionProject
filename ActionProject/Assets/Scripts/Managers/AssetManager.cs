using Action.Util;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;

namespace Action.Manager
{
    public enum eAssetType
    {
        NONE,
        SINGLETON,
        UI
    }

    public class AssetManager : Singleton<AssetManager>
    {
        Dictionary<string, GameObject> _assetDic;

        public Dictionary<string, GameObject> AssetDic => _assetDic;

        public override void Initialize()
        {
            if (null == _assetDic) { _assetDic = new Dictionary<string, GameObject>(); }
        }

        public GameObject LoadAsset(eAssetType type, string prefabName)
        {
            string path = GetAssetPath(type, prefabName);

            GameObject ret = Instantiate(Resources.Load(path)) as GameObject;
            return ret;
        }

        public async UniTask<GameObject> LoadAssetAsync(eAssetType type, string prefabName)
        {
            string path = GetAssetPath(type, prefabName);
            ResourceRequest request = Resources.LoadAsync<GameObject>(path);
            await request;
            GameObject ret = request.asset as GameObject;
            return ret;
        }

        string GetAssetPath(eAssetType type, string prefabName)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string mainPath = "Prefabs/";
            string typePath = "";

            switch (type)
            {
                case eAssetType.NONE:
                    typePath = "";
                    break;
                case eAssetType.SINGLETON:
                    typePath = "Singleton/";
                    break;
                case eAssetType.UI:
                    typePath = "UI/";
                    break;
            }

            string ret = stringBuilder.Append(mainPath).Append(typePath).Append(prefabName).ToString();
            return ret;
        }
    }
}