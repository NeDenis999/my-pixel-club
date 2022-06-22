﻿using System;
using Battle;
using UnityEngine;

public class StartBattleTestAnimation : MonoBehaviour
{
    public BattleController BattleController;

    private void Start()
    {
        BattleController.gameObject.SetActive(true);
        BattleController.StartFight();
    }
}