using Managers;
using Zenject;

namespace Installers
{
    public class LevelSaverInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelSaver>().AsSingle().NonLazy();
        }
    }
}
