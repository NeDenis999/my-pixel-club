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
        
        private DataSaveLoadService _dataSaveLoadService;
        private AssetProviderService _assetProviderService;
        
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindPlayerData();
            InitAllService();
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
            _dataSaveLoadService = new DataSaveLoadService(_allCards, _avatars);
            
            Container
                .Bind<DataSaveLoadService>()
                .FromInstance(_dataSaveLoadService)
                .AsSingle();
            
            _dataSaveLoadService.Load();
        }
        
        private void InitAllService()
        {
            AllServices.AssetProviderService = _assetProviderService;
            AllServices.DataSaveLoadService = _dataSaveLoadService;
        }
    }
}