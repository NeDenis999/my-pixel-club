using System;
using System.Runtime.InteropServices;
using Data;
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
    }
    
    private void Start()
    { 
        TurnOffAllPages();
        _startPage.Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            _data.Save();

        if (Input.GetKeyDown(KeyCode.Keypad2))
            _data.Load();
        
        if (Input.GetKeyDown(KeyCode.Keypad3))
            _data.SetCoinCount(1000);
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