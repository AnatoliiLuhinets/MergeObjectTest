using Managers;
using Zenject;

namespace Installers
{
    public class ObjectTrackerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ObjectsTracker>().AsSingle().NonLazy();
        }
    }
}
