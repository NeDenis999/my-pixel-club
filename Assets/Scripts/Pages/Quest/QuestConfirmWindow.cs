using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestConfirmWindow : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AttackDeck _attackDeck;

    [SerializeField] private int _requiredAmountEnergy;

    [SerializeField] private GameObject _questList, _quest, _exeptionBaner;

    [SerializeField] private TMP_Text _exeptionBanerText;

    public int RequiredAmountEnergy => _requiredAmountEnergy;

    public void StartQuest()
    {
        if (_requiredAmountEnergy <= _player.Energy)
        {
            if (CheckForDeckEmpty() == false)
                CheckForPlayerAlive();
            else
                OpenExeptionBaner("Not card in deck");
        }
        else
        {
            OpenExeptionBaner("Not enough energy");
        }
        gameObject.SetActive(false);
    }

    private void CheckForPlayerAlive()
    {
        if (_player.Health > 0)
        {
            _quest.gameObject.SetActive(true);
            _questList.gameObject.SetActive(false);
        }
        else
        {
            OpenExeptionBaner("Not enough health");
        }
    }

    private bool CheckForDeckEmpty()
    {
        foreach (var item in _attackDeck.CardsInDeck)
        {
            if (item.Card.Rarity != RarityCard.Epmpty)
                return false;
        }

        return true;
    }

    private void OpenExeptionBaner(string exeptionName)
    {
        _exeptionBaner.SetActive(true);
        _exeptionBanerText.text = exeptionName;
    }
}
