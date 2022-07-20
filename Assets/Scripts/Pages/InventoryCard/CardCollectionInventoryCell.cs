using Cards.CardCell;
using UnityEngine;
using UnityEngine.UI;

namespace FarmPage.InventoryCard
{
    public class CardCollectionInventoryCell : CardCell
    {
        [SerializeField]
        private CardCellInDeck _cellInDeck;

        [SerializeField] 
        private Button _button;
        
        private StatisticWindow _statisticWindow;

        public Sprite UIIcon => Card.UIIcon;

        public string Statistic => "Name: " + Card.Name + "\nLevel: " + Level + "\nAtk: " + Attack.ToString() +
                                   "\nDef: " + Def + "\nRace: " + Card.Race + "\nSkill: " + Card.AttackSkillName +
                                   "\nSkill chance: " + Card.SkillChance + " %";

        public string Discription => Card.Discription;

        public BottleEffects Effect => BottleEffects.None;

        public void Initialization(StatisticWindow statisticWindow, Card card)
        {
            _button.onClick.AddListener(SetCardInDeck);
            _statisticWindow = statisticWindow;
            
            _cellInDeck.Initialize(_card, _statisticWindow);
            _card = card;
        }
        
        private void SetCardInDeck()
        {
            _statisticWindow.Render(_cellInDeck);
            print("open");
            _statisticWindow.gameObject.SetActive(true);
        }
    }
}
