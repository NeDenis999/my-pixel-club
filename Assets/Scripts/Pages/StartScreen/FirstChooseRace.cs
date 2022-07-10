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
        private SceneLoadService _sceneLoadService;
        
        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, SceneLoadService sceneLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _sceneLoadService = sceneLoadService;
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
            _sceneLoadService.WaitLoadScene.allowSceneActivation = true;
        }
    }
}