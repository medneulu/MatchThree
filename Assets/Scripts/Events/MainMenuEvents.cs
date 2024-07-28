using UnityEngine.Events;

namespace Events
{
    public  class MainMenuEvents
    {
        public UnityAction SettingsBTN;
        public UnityAction SettingsExitBTN;
        public UnityAction StartBTN;
        public UnityAction<float> SoundValueChanged;
        public UnityAction<float> MusicValueChanged;
        public UnityAction<bool> VibrationValueChanged;
    }
}