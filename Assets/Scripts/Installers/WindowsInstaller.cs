using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class WindowsInstaller : MonoInstaller
    {
        [SerializeField] private SettingsWindow _settingsWindow;

        public override void InstallBindings()
        {
            Container.Bind<SettingsWindow>().FromInstance(_settingsWindow).AsSingle().NonLazy();
        }
    }
}
