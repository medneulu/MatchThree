using System;
using Events;
using UnityEngine;
using Utils;
using Zenject;

namespace Datas
{
    [Serializable]
    public class PlayerData : IInitializable
    {
        [Inject] private MainMenuEvents MainMenuEvents { get; set; }
        [Inject] private GridEvents GridEvents { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        public float SoundVal => _soundVal;
        public float MusicVal => _musicVal;
        public bool VibrationVal => _vibrationVal; 
        private const string SoundPrefKey = "Sound";
        private const string MusicPrefKey = "Music";
        private const string VibrationPrefKey = "Vibration";
        private const string Plevel = "PLevel";
        public int CurrScore { get; private set; }
        public float CurrTime { get; set; }
        [SerializeField] private float _soundVal;
        [SerializeField] private float _musicVal;
        [SerializeField] private bool _vibrationVal;

        [SerializeField] private int _pLevel;

        public int CurrLevel
        {
            get => _pLevel;
            set => _pLevel = value;
        }
        
        public void SetScore(int score)
        {
            CurrScore = score;
        }
        private void RegisterEvents()
        {
            MainMenuEvents.SoundValueChanged += OnSoundValueChanged;
            MainMenuEvents.MusicValueChanged += OnMusicValueChanged;
            MainMenuEvents.VibrationValueChanged += OnVibrationValueChanged;
            GridEvents.MatchGroupDespawn += OnMatchGroupDespawn;
            PlayerEvents.LevelComplete += OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            _pLevel++;
            CurrScore = 0;
            PlayerPrefs.SetInt(Plevel, _pLevel);
        }

        private void OnMatchGroupDespawn(int arg0)
        {
            CurrScore += arg0;
            PlayerEvents.PlayerScoreChanged?.Invoke();
        }

        private void OnVibrationValueChanged(bool isActive)
        {
            _vibrationVal = isActive;
            PlayerPrefs.SetInt(VibrationPrefKey, isActive.ToInt());
        }

        private void OnSoundValueChanged(float soundVal)
        {
            _soundVal = soundVal;
            PlayerPrefs.SetFloat(SoundPrefKey, _soundVal);
        }
        
        private void OnMusicValueChanged(float musicVal)
        {
            _musicVal = musicVal;
            PlayerPrefs.SetFloat(MusicPrefKey, _musicVal);
        }

        public void Initialize()
        {
            _soundVal = PlayerPrefs.GetFloat(SoundPrefKey);
            _musicVal = PlayerPrefs.GetFloat(MusicPrefKey);
            _vibrationVal = PlayerPrefs.GetInt(VibrationPrefKey).ToBool();
            _pLevel = PlayerPrefs.GetInt(Plevel);
            RegisterEvents();
        }
    }
}