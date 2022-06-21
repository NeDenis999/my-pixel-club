using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatistic : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _energyText;

    private void Start()
    {
        _player.OnLevelChange += (int level) => _levelText.text = level.ToString() + "/100";
        _player.OnEnergyChange += (int energy) => _energyText.text = energy.ToString() + "/25";
    }
}
