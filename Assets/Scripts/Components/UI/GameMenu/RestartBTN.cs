using Events;
using UnityEngine;
using Utils;
using Zenject;

namespace UI.OnGameMenu
{
    public class RestartBTN : UIBTN
    {
        [Inject] private GameMenuEvents GameMenuEvents { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        
        protected override void OnClick()
        {
            GameMenuEvents.RestartBTN?.Invoke();
        }
    }
}