using Data;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ObjectDataLibraryInstaller : MonoInstaller
    {
        [SerializeField] private ObjectDataLibrary _objectDataLibrary;

        public override void InstallBindings()
        {
            Container.Bind<ObjectDataLibrary>().FromInstance(_objectDataLibrary).AsSingle().NonLazy();
        }
    }
}
