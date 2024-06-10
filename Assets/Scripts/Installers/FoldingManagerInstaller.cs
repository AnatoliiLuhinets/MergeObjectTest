using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FoldingManagerInstaller : MonoInstaller
    {
        [SerializeField] private FoldingManager _foldingManager;
        
        public override void InstallBindings()
        {
            Container.Bind<FoldingManager>().FromInstance(_foldingManager).AsSingle().NonLazy();
        }
    }
}
