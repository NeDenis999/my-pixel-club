using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace FarmPage.StartScreen
{
    public class StartPage : MonoBehaviour
    {
        [SerializeField] 
        private Page _currentPage;
        
        [SerializeField]
        private ChooseNickname _chooseNickname;
        
        private DataSaveLoadService _dataSaveLoadService;
        private SceneLoadService _sceneLoadService;
        private SoundService _soundService;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, SceneLoadService sceneLoadService, SoundService soundService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _sceneLoadService = sceneLoadService;
            _soundService = soundService;
        }

        private void Start()
        {
            _sceneLoadService.StartAsyncLoadScene(1);
            _soundService.PlayMainTheme();
        }

        public void StartGame()
        {
            print("StartGame");
            
            //if (!_waitLoadScene.isDone)
                //return;
            
            if (_dataSaveLoadService.PlayerData.Species == Species.None)
            {
                _currentPage.Hide();
                _chooseNickname.gameObject.SetActive(true);
            }
            else
            {
                _sceneLoadService.WaitLoadScene.allowSceneActivation = true;
            }
        }
    }
}