using System.Collections.Generic;
using Components;
using Datas;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = nameof(ProjectSettings), menuName = EnvVar.ProjectSettingsPath, order = 0)]
    public class ProjectSettings : ScriptableObject
    {
        
        [SerializeField] private PlayerData _playerDataView;
        [SerializeField] private GridManager.Settings _gridManagerSettings;
        [SerializeField] private List<GameObject> _levelList;
        public List<GameObject> LevelList => _levelList;
        public GridManager.Settings GridManagerSettings => _gridManagerSettings;

        public void SetPlayerDataView(PlayerData playerData)
        {
            _playerDataView = playerData;
        }

        public GameObject GetLevel(int pLevel)
        {
            return _levelList[pLevel];
        }
    }
}