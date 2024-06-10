using Managers;
using Zenject;

namespace Installers
{
    public class ScoreManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ScoreManager>().AsSingle().NonLazy();
        }
    }
}
