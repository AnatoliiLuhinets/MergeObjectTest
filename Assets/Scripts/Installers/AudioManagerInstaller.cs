using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class AudioManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().AsSingle().NonLazy();
        }
    }
}
