using Datas;
using Events;
using Extensions.Unity.MonoHelper;
using UnityEngine;
using Zenject;

namespace UI.OnGame
{
    public class GameMenuManager : EventListenerMono
    {
        [Inject] private GameMenuEvents GameMenuEvents { get; set; }
        [Inject] private MainUIEvents MainUIEvents { get; set; }
        [Inject] private PlayerData PlayerData { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        [SerializeField] private GameObject _completePanel;
        [SerializeField] private GameObject _pauseMenuPanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _timerGameObject;
        private PlayerTimerTMP _playerTimerTMP;
        private AudioManager _audioManager;
        
        private void Awake()
        {
            _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            _playerTimerTMP = _timerGameObject.GetComponent<PlayerTimerTMP>();
        }
        private void Start()
        {
            CloseAllPanels();
        }
        
        private void SetPanelActive(GameObject panel)
        {
            _completePanel.SetActive(_completePanel == panel);
            _pauseMenuPanel.SetActive(_pauseMenuPanel == panel);
            _gameOverPanel.SetActive(_gameOverPanel == panel);
        }
        
        
        private void CloseAllPanels()
        {
            SetPanelActive(null);
        }

        protected override void RegisterEvents()
        {
            GameMenuEvents.RestartBTN += OnRestartBTN;
            GameMenuEvents.ResumeBTN += OnResumeBTN;
            GameMenuEvents.PauseBTN += OnPauseBTN;
            GameMenuEvents.NextLevelBTN += OnNextLevelBTN;
            PlayerEvents.TimerOut += OnTimerOut;
            PlayerEvents.LevelComplete += OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            SetPanelActive(_completePanel);
        }

        private void OnTimerOut()
        {
            SetPanelActive(_gameOverPanel);
        }

        private void OnNextLevelBTN()
        {
            MainUIEvents.ButtonClick?.Invoke();
            CloseAllPanels();
        }

        private void OnPauseBTN()
        {
            MainUIEvents.ButtonClick?.Invoke();
            SetPanelActive(_pauseMenuPanel);
        }

        private void OnRestartBTN()
        {
            MainUIEvents.ButtonClick?.Invoke();
            CloseAllPanels();
            PlayerData.SetScore(0);
        }

        private void OnResumeBTN()
        {
            MainUIEvents.ButtonClick?.Invoke();
            CloseAllPanels();
        }

        private void OnExitBTN()
        {
            MainUIEvents.ButtonClick?.Invoke();
            CloseAllPanels();
        }
        protected override void UnRegisterEvents()
        {
            GameMenuEvents.RestartBTN -= OnRestartBTN;
            GameMenuEvents.ResumeBTN -= OnResumeBTN;
            GameMenuEvents.PauseBTN -= OnPauseBTN;
            GameMenuEvents.NextLevelBTN -= OnNextLevelBTN;
            PlayerEvents.TimerOut -= OnTimerOut;
            PlayerEvents.LevelComplete -= OnLevelComplete;
        }
    }
}