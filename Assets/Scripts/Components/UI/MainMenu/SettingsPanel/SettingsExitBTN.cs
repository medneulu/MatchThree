using Events;
using Utils;
using Zenject;

namespace UI.MainMenu.SettingsPanel
{
    public class SettingsExitBTN : UIBTN
    {
        [Inject] private MainMenuEvents MainMenuEvents { get; set; }
        protected override void OnClick()
        {
            MainMenuEvents.SettingsExitBTN?.Invoke();
        }
    }
}