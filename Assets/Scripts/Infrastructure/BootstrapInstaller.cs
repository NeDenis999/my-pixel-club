using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField]
        private Card[] _allCards;
        
        [SerializeField]
        private Sprite[] _avatars;
        
        [SerializeField]
        private Sprite[] _frames;
        
        private DataSaveLoadService _data;
        private AssetProviderService _assetProviderService;
        
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindPlayerData();
        }

        private void BindAssetProvider()
        {
            _assetProviderService = new AssetProviderService(_frames, _allCards);
            
            Container
                .Bind<AssetProviderService>()
                .FromInstance(_assetProviderService)
                .AsSingle();
        }

        private void BindPlayerData()
        {
            _data = new DataSaveLoadService(_allCards, _avatars);
            
            Container
                .Bind<DataSaveLoadService>()
                .FromInstance(_data)
                .AsSingle();
            
            _data.Load();
        }
    }
}