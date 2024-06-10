using System;
using System.Text.RegularExpressions;
using Environment;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/Data/ObjectData")]
    public class ObjectData : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public FoldingItemsType ObjectType { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        
        [ContextMenu("Update ID")]
        private void UpdateID()
        {
            var guid = Guid.NewGuid();
            var newID = Convert.ToBase64String(guid.ToByteArray());
            newID = Regex.Replace(newID, "[^a-zA-Z0-9]", "");
            ID = newID.Substring(0, Math.Min(15, newID.Length));
        }
    }
}
