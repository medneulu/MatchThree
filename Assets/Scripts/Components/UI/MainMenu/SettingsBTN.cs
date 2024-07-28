using Events;
using Utils;
using Zenject;

namespace UI.MainMenu
{
    public class SettingsBTN : UIBTN
    {
        [Inject] private MainMenuEvents MainMenuEvents { get; set; }
        protected override void OnClick()
        {
            MainMenuEvents.SettingsBTN?.Invoke();
        }
    }
}