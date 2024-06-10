using Managers;
using Zenject;

namespace Installers
{
    public class ObjectsSpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ObjectsSpawner>().AsSingle().NonLazy();
        }
    }
}
