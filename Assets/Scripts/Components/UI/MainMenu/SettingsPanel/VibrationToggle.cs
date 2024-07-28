using Datas;
using Events;
using Installers;
using Utils;
using Zenject;

namespace UI.MainMenu.SettingsPanel
{
    public class VibrationToggle : UIToggle
    {
        [Inject] private MainMenuEvents MainMenuEvents { get; set; }
        [Inject] private PlayerData PlayerData { get; set; }
        protected override void OnEnable()
        {
            base.OnEnable();
            _toggle.isOn = PlayerData.VibrationVal;
        }

        protected override void OnValueChanged(bool isActive)
        {
            MainMenuEvents.VibrationValueChanged?.Invoke(isActive);
        }
    }
}