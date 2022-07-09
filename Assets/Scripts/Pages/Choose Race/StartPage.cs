using Data;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Pages.Choose_Race
{
    public class StartPage : MonoBehaviour
    {
        [SerializeField] 
        private Page _currentPage;
        
        [SerializeField] 
        private Page _chooseRacePage;
        
        private DataSaveLoadService _dataSaveLoadService;
        
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }

        public void StartGame()
        {
            print(_dataSaveLoadService);
            
            if (_dataSaveLoadService.PlayerData.Species == Species.None)
            {
                _currentPage.Hide();
                _chooseRacePage.StartShowSmooth();
            }
            else
                SceneManager.LoadScene(1);
        }
    }
}