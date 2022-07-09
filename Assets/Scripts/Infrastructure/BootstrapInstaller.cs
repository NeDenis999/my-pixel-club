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
        private LocalDataService _localDataService;
        
        public override void InstallBindings()
        {
            BindAssetProvider();
            BindDataSaveLoad();
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

        private void BindDataSaveLoad()
        {
            _dataSaveLoadService = new DataSaveLoadService(_allCards, _avatars);
            
            Container
                .Bind<DataSaveLoadService>()
                .FromInstance(_dataSaveLoadService)
                .AsSingle();
            
            _dataSaveLoadService.Load();
        }

        private void BindPlayerData()
        {
            _localDataService = new LocalDataService(_dataSaveLoadService);
            
            Container
                .Bind<LocalDataService>()
                .FromInstance(_localDataService)
                .AsSingle();
        }

        private void InitAllService()
        {
            AllServices.AssetProviderService = _assetProviderService;
            AllServices.DataSaveLoadService = _dataSaveLoadService;
            AllServices.LocalDataService = _localDataService;
        }
    }
}