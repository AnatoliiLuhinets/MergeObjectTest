using Managers;
using Zenject;

namespace Installers
{
    public class SaveServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveService>().AsSingle().NonLazy();
        }
    }
}
