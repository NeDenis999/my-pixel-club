using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Pages.StartScreen
{
    public class StartPage : MonoBehaviour
    {
        [SerializeField] 
        private Page _currentPage;
        
        [SerializeField] 
        private Page _chooseRacePage;
        
        private DataSaveLoadService _dataSaveLoadService;
        private SceneLoadService _sceneLoadService;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, SceneLoadService sceneLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _sceneLoadService = sceneLoadService;
        }

        private void Start()
        {
            _sceneLoadService.StartAsyncLoadScene(1);
        }

        public void StartGame()
        {
            print("StartGame");
            
            //if (!_waitLoadScene.isDone)
                //return;
            
            if (_dataSaveLoadService.PlayerData.Species == Species.None)
            {
                _currentPage.Hide();
                _chooseRacePage.StartShowSmooth();
            }
            else
            {
                _sceneLoadService.WaitLoadScene.allowSceneActivation = true;
            }
        }
    }
}