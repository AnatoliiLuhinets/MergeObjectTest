using System.Collections.Generic;
using System.Linq;
using Environment;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ObjectDataLibrary", menuName = "ScriptableObjects/Data/ObjectDataLibrary")]
    public class ObjectDataLibrary : ScriptableObject
    {
        [field: SerializeField] public List<ObjectData> DataLibrary { get; private set; } = new List<ObjectData>();

        public ObjectData GetObjectDataByID(string ID)
        {
            return DataLibrary.FirstOrDefault((item) => item.ID == ID);
        }

        public ObjectData GetObjectDataByType(FoldingItemsType type)
        {
            return DataLibrary.FirstOrDefault((item) => item.ObjectType == type);
        }
    }
}
