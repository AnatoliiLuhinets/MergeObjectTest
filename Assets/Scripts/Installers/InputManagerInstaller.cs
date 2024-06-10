using Core.Input;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InputManagerInstaller : MonoInstaller
    {
        [SerializeField] private InputManager _inputManager;

        public override void InstallBindings()
        {
            Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle().NonLazy();
        }
    }
}
