using System;
using Datas;
using Events;
using Settings;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private Camera _camera;
        [Inject] private ProjectSettings ProjectSettings { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        [Inject] private PlayerData PlayerData { get; set; }

        [SerializeField] private int _pLevel;
        
        private int _requireScore;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_camera);
        }
        
        public override void Start()
        {
            PlayerData.CurrLevel = PlayerData.CurrLevel < ProjectSettings.LevelList.Count ? PlayerData.CurrLevel : 0; 
            GameObject newLevelPrefab =  ProjectSettings.GetLevel(PlayerData.CurrLevel); 
            ControlLevel();
            Container.InstantiatePrefab(newLevelPrefab);
        }

        private void Update()
        {
            Debug.Log("player data" + PlayerData.CurrLevel);
        }

        private void OnEnable()
        {
            ControlLevel();
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void ControlLevel()
        {
            switch (PlayerData.CurrLevel)
            {
                case 0:
                    _requireScore = 50;
                    PlayerData.CurrTime = 30;
                    break;
                case 1:
                    _requireScore = 100;
                    PlayerData.CurrTime = 60;
                    break;
                case 2:
                    _requireScore = 150;
                    PlayerData.CurrTime = 90;
                    break;
                default:
                    _requireScore = 50;
                    PlayerData.CurrTime = 30;
                    break;
            }
        }

        private void CheckLevelComplete()
        {
            if (PlayerData.CurrScore > _requireScore)
            {
                PlayerEvents.LevelComplete?.Invoke();
                Time.timeScale = 0;
            }
        }

        private void RegisterEvents()
        {
            PlayerEvents.PlayerScoreChanged += OnPlayerScoreChanged;
        }

        private void OnPlayerScoreChanged()
        {
            CheckLevelComplete();
        }

        private void UnRegisterEvents()
        {
            PlayerEvents.PlayerScoreChanged -= OnPlayerScoreChanged;
        }
    }
}