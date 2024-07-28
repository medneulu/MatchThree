using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;
using Zenject;

public class PauseMenuManager : EventListenerMono
{
    [Inject] private GameMenuEvents GameMenuEvents { get; set; }
    [SerializeField] private GameObject _onGameMenuPanel;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _completeMenuPanel;
    [SerializeField] private GameObject _gameOverMenuPanel;
    
    private void Start()
    {
        SetPanelActive(_onGameMenuPanel);
    }

    private void SetPanelActive(GameObject panel)
    {
        _onGameMenuPanel.SetActive(_onGameMenuPanel == panel);
        _pauseMenuPanel.SetActive(_pauseMenuPanel == panel);
    }
    
    protected override void RegisterEvents()
    {
        GameMenuEvents.PauseBTN += OnPauseBTN;
    }

    private void OnPauseBTN()
    {
        SetPanelActive(_pauseMenuPanel);
    }
    
    protected override void UnRegisterEvents()
    {
        GameMenuEvents.PauseBTN -= OnPauseBTN;
    }
}
