using Data;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace FarmPage.StartScreen
{
    public class FirstChooseRace : MonoBehaviour
    {
        [SerializeField] 
        private Card[] _manStartCards;
        
        [SerializeField] 
        private Card[] _demonStartCards;
        
        [SerializeField]
        private Card[] _goodStartCards;
        
        private DataSaveLoadService _dataSaveLoadService;
        private SceneLoadService _sceneLoadService;

        [Inject]
        private void Construct(DataSaveLoadService dataSaveLoadService, SceneLoadService sceneLoadService)
        {
            _dataSaveLoadService = dataSaveLoadService;
            _sceneLoadService = sceneLoadService;
        }

        public void ChooseHuman() => 
            ChooseSpecies(Species.Human, _manStartCards);
        
        public void ChooseDemon() => 
            ChooseSpecies(Species.Demon, _demonStartCards);
        
        public void ChooseGod() => 
            ChooseSpecies(Species.God, _goodStartCards);

        private void ChooseSpecies(Species species, Card[] startCards)
        {
            CardData[] cardsData = new CardData[5];

            for (int i = 0; i < startCards.Length; i++) 
                cardsData[i] = startCards[i].GetCardData();

            _dataSaveLoadService.SetSpecies(species);
            _dataSaveLoadService.SetAttackDecks(cardsData);
            _sceneLoadService.WaitLoadScene.allowSceneActivation = true;
        }
    }
}