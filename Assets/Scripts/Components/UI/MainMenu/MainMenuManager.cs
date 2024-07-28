using Events;
using UnityEngine;
using Utils;
using Zenject;

namespace UI.MainMenu
{
    public class MainMenuManager : EventListenerMono
    {
        [Inject] private MainMenuEvents MainMenuEvents { get; set; }
        [Inject] private MainUIEvents MainUIEvents { get; set; }
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _settingsMenuPanel;
        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
        private void Start()
        {
            SetPanelActive(_mainMenuPanel);
        }

        private void SetPanelActive(GameObject panel)
        {
            _mainMenuPanel.SetActive(_mainMenuPanel == panel);
            _settingsMenuPanel.SetActive(_settingsMenuPanel == panel);
        }
        
        protected override void RegisterEvents()
        {
            MainMenuEvents.SettingsBTN += OnSettingsBTN;
            MainMenuEvents.SettingsExitBTN += OnSettingsExitBTN;
        }

        private void OnSettingsBTN()
        {
            MainUIEvents.ButtonClick?.Invoke();
            SetPanelActive(_settingsMenuPanel);
        }

        private void OnSettingsExitBTN()
        {
            MainUIEvents.ButtonClick?.Invoke();
            SetPanelActive(_mainMenuPanel);
        }

        protected override void UnRegisterEvents()
        {
            MainMenuEvents.SettingsBTN -= OnSettingsBTN;
            MainMenuEvents.SettingsExitBTN -= OnSettingsExitBTN;
        }
    }
}