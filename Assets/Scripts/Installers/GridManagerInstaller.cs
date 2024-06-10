using Managers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GridManagerInstaller : MonoInstaller
    {
        [SerializeField] private GridManager _gridManager;

        public override void InstallBindings()
        {
            Container.Bind<GridManager>().FromInstance(_gridManager).AsSingle().NonLazy();
        }
    }
}
