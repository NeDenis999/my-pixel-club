using Data;
using Infrastructure.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Pages.Choose_Race
{
    public class FirstChooseRace : MonoBehaviour
    {
        private DataSaveLoadService _dataSaveLoadService;
        
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
        }

        public void ChooseHuman() => 
            ChooseSpecies(Species.Human);
        
        public void ChooseDemon() => 
            ChooseSpecies(Species.Demon);
        
        public void ChooseGod() => 
            ChooseSpecies(Species.God);

        private void ChooseSpecies(Species species)
        {
            _dataSaveLoadService.SetSpecies(species);
            SceneManager.LoadScene(1);
        }
    }
}