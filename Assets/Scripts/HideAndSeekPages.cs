using UnityEngine;

public class HideAndSeekPages : MonoBehaviour
{
    [SerializeField] 
    private Page[] _pages;

    [SerializeField] 
    private Page _startPage;
    
    private void Start()
    { 
        TurnOffAllPages();
        _startPage.Show();
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