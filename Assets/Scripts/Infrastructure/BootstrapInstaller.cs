using Data;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private Card _emptyCard;
        
        private DataSaveLoadService _data;
        
        public override void InstallBindings()
        {
            BindPlayerData();
        }

        private void BindPlayerData()
        {
            _data = new DataSaveLoadService(_emptyCard);
            
            Container
                .Bind<DataSaveLoadService>()
                .FromInstance(_data)
                .AsSingle();
            
            _data.Load();
        }
    }
}