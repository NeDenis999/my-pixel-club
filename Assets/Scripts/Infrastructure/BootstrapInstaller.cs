using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private Card _emptyCard;
        
        [SerializeField]
        private Sprite[] _avatars;
        
        [SerializeField]
        private Sprite[] _frames;
        
        private DataSaveLoadService _data;
        private AssetProviderService _assetProviderService;
        
        public override void InstallBindings()
        {
            BindPlayerData();
            BindAssetProvider();
        }

        private void BindPlayerData()
        {
            _data = new DataSaveLoadService(_emptyCard, _avatars);
            
            Container
                .Bind<DataSaveLoadService>()
                .FromInstance(_data)
                .AsSingle();
            
            _data.Load();
        }

        private void BindAssetProvider()
        {
            _assetProviderService = new AssetProviderService(_frames);
            
            Container
                .Bind<AssetProviderService>()
                .FromInstance(_assetProviderService)
                .AsSingle();
        }
    }
}