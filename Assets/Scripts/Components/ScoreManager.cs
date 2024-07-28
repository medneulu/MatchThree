using Datas;
using Events;
using Installers;
using Zenject;

namespace Components
{
    public class ScoreManager
    {
        
        [Inject] private PlayerData PlayerData { get; set; }
        [Inject] private MainSceneInstaller MainSceneInstaller { get; set; }
        [Inject] private GameMenuEvents GameMenuEvents { get; set; }
        
    }
}