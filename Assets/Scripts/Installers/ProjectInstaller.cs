using Datas;
using Events;
using Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        private ProjectEvents _projectEvents;
        private InputEvents _inputEvents;
        private GridEvents _gridEvents;
        private ProjectSettings _projectSettings;
        private PlayerData _playerData;
        private MainMenuEvents _mainMenuEvents;
        private MainUIEvents _mainUIEvents;
        private GameMenuEvents _gameMenuEvents;
        private PlayerEvents _playerEvents;
        [SerializeField] private GameObject _auidoManagerPref;

  
        public override void InstallBindings()
        {
            InstallEvents();
            InstallSettings();
            InstallData();
            InstallMono();
        }

        private void InstallMono()
        {
            GameObject audioManagerGameObject = Container.InstantiatePrefab(_auidoManagerPref);
            AudioManager audioManager = audioManagerGameObject.GetComponent<AudioManager>();
            Container.BindInstance(audioManager).AsSingle();
        }

        private void InstallData()
        {
            Container.BindInterfacesAndSelfTo<PlayerData>().AsSingle();
        }

        private void InstallEvents()
        {
            _projectEvents = new ProjectEvents();
            Container.BindInstance(_projectEvents).AsSingle();
            
            _inputEvents = new InputEvents();
            Container.BindInstance(_inputEvents).AsSingle();

            _gridEvents = new GridEvents();
            Container.BindInstance(_gridEvents).AsSingle();

            _mainMenuEvents = new MainMenuEvents();
            Container.BindInstance(_mainMenuEvents).AsSingle();

            _mainUIEvents = new MainUIEvents();
            Container.BindInstance(_mainUIEvents).AsSingle();
            
            _gameMenuEvents = new GameMenuEvents();
            Container.BindInstance(_gameMenuEvents).AsSingle();

            _playerEvents = new PlayerEvents();
            Container.BindInstance(_playerEvents).AsSingle();
            
            
        }

        private void InstallSettings()
        {
            _projectSettings = Resources.Load<ProjectSettings>(EnvVar.ProjectSettingsPath);
            Container.BindInstance(_projectSettings).AsSingle();
            
        }

        private void Awake()
        {
            RegisterEvents();
        }

        public override void Start()
        {
            _playerData = Container.Resolve<PlayerData>();
            _projectSettings.SetPlayerDataView(_playerData);
            _projectEvents.ProjectStarted?.Invoke();
            
        }

        private static void LoadScene(string sceneName) {SceneManager.LoadScene(sceneName);}

        private void RegisterEvents()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            _mainMenuEvents.StartBTN += OnStartBTN;
            _mainUIEvents.ExitBTN += OnExitBTN;
            _gameMenuEvents.RestartBTN += OnRestartBTN;
            _gameMenuEvents.ExitBTN += OnExitBTN;
            _gameMenuEvents.ResumeBTN += OnResumeBTN;
            _gameMenuEvents.PauseBTN += OnPauseBTN;
            _gameMenuEvents.NextLevelBTN += OnNextLevelBTN;
        }

        private void OnNextLevelBTN()
        {
            LoadScene("Main");
        }

        private void OnExitBTN()
        {
            LoadScene("MainMenu");
        }

        private void OnStartBTN()
        {
            LoadScene("Main");
        }

        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode arg1)
        {
            if(loadedScene.name == EnvVar.LoginSceneName)
            {
                LoadScene(EnvVar.MainSceneName);
            }
        }
        private void OnRestartBTN()
        {
            LoadScene("Main");
        }

        private void OnResumeBTN()
        {
            _playerEvents.PlayerTimerStoped?.Invoke(false, 0); 
        }

        private void OnPauseBTN()
        {
            _playerEvents.PlayerTimerStoped?.Invoke(true, 0);
        }
    }
}