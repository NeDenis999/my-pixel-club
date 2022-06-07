using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    [SerializeField] private GameObject[] _scenes;

    private void Start()
    { 
        TurnOffAllPages();
    }

    public void TurnOffAllPages()
    {
        for (int i = 0; i < _scenes.Length; i++)
        {
            _scenes[i].SetActive(false);
        }
    }
}