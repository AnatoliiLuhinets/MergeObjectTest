using System;
using System.Collections.Generic;
using System.IO;
using Common;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class SceneItem
    {
        [field: SerializeField] public string ObjectID { get; set; }
        [field: SerializeField] public int CellID { get; set; }

        public SceneItem(string objectID, int cellID)
        {
            ObjectID = objectID;
            CellID = cellID;
        }
    }
    
    [Serializable]
    public class SceneItems
    {
        [field: SerializeField] public List<SceneItem> Items { get; set; } = new List<SceneItem>();
    }
    
    public class SaveService
    {
        public async UniTask SaveLevelProgress(List<SceneItem> items)
        {
            var json = JsonUtility.ToJson(new SceneItems { Items = items }, true);
            await File.WriteAllTextAsync(Consts.SaveSystem.LevelProgress, json);
        }
        
        public async UniTask<List<SceneItem>> LoadLevelProgress()
        {
            if (!File.Exists(Consts.SaveSystem.LevelProgress))
            {
                return new List<SceneItem>();
            }

            var json = await File.ReadAllTextAsync(Consts.SaveSystem.LevelProgress);
            var sceneItemList = JsonUtility.FromJson<SceneItems>(json);
            return sceneItemList.Items;
        }
    }
}
