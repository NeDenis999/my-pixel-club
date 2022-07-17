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

        [SerializeField] 
        private ShopItemBottle[] _items;

        [SerializeField] 
        private AudioSource _audioSource;

        [SerializeField] 
        private AudioClip[] _audioClips;
        
        [SerializeField] 
        private Texture2D _cursorImage;
        
        [SerializeField] 
        private Texture2D _cursorClickImage;
        
        [SerializeField] 
        private Sprite[] _allNFT;
        
        private CoroutineStarterService _coroutineStarterService;
        private DataSaveLoadService _dataSaveLoadService;
        private AssetProviderService _assetProviderService;
        private LocalDataService _localDataService;
        private SceneLoadService _sceneLoadService;
        private SoundService _soundService;
        
        public override void InstallBindings()
        {
            BindCoroutineStarter();
            BindAssetProvider();
            BindDataSaveLoad();
            BindPlayerData();
            BindSceneLoad();
            BindSound();
            InitAllService();
        }

        private void BindCoroutineStarter()
        {
            _coroutineStarterService = new CoroutineStarterService(this);
            
            Container
                .Bind<CoroutineStarterService>()
                .FromInstance(_coroutineStarterService)
                .AsSingle();
        }

        private void BindAssetProvider()
        {
            _assetProviderService = new AssetProviderService(_frames, _allCards, _items, _cursorImage, _cursorClickImage, _allNFT);
            
            Container
                .Bind<AssetProviderService>()
                .FromInstance(_assetProviderService)
                .AsSingle();
        }

        private void BindDataSaveLoad()
        {
            _dataSaveLoadService = new DataSaveLoadService(_allCards, _avatars, _items);
            
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

        private void BindSceneLoad()
        {
            _sceneLoadService = new SceneLoadService(_coroutineStarterService);
            
            Container
                .Bind<SceneLoadService>()
                .FromInstance(_sceneLoadService)
                .AsSingle();
        }

        private void BindSound()
        {
            _soundService = new SoundService(_audioSource, _audioClips);
            
            Container
                .Bind<SoundService>()
                .FromInstance(_soundService)
                .AsSingle();
        }
        
        private void InitAllService()
        {
            AllServices.AssetProviderService = _assetProviderService;
            AllServices.DataSaveLoadService = _dataSaveLoadService;
            AllServices.LocalDataService = _localDataService;
            AllServices.SceneLoadService = _sceneLoadService;
            AllServices.CoroutineStarterService = _coroutineStarterService;
            AllServices.SoundService = _soundService;
        }
    }
}