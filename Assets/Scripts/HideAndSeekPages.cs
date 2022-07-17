using Data;
using Infrastructure.Services;
using System;
using UnityEngine;
using Zenject;

public class HideAndSeekPages : MonoBehaviour
{
    [SerializeField] 
    private Page[] _pages;

    [SerializeField] 
    private Page _startPage;

    private DataSaveLoadService _data;

    [Inject]
    private void Construct(DataSaveLoadService data)
    {
        _data = data;
        
        if (_data.PlayerData.Coins == 0)
            _data.SetCoinCount(1000);
    }
    
    private void Start()
    {
        InitAllPages();
        ShowAllPages();
        TurnOffAllPages();
        _startPage.Show();
    }

    private void InitAllPages()
    {
        foreach (var page in _pages)
            page.InitStartPosition();
    }

    private void ShowAllPages()
    {
        try
        {
            foreach (var page in _pages)
                page.Show();
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
        }
    }

    public void TurnOffAllPages()
    {
        for (int i = 0; i < _pages.Length; i++) 
            if (_pages[i].gameObject.activeSelf) 
                _pages[i].Hide();
    }
    
    public void TurnOffAllPagesSmooth()
    {
        for (int i = 0; i < _pages.Length; i++)
        {
            if (_pages[i].gameObject.activeSelf) 
                _pages[i].StartHideSmooth();
        }
    }
}