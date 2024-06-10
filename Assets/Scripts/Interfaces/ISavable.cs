using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Interfaces
{
    public interface ISavable<T>
    {
        void Save();
        UniTask<List<T>> Load();
    }
}
