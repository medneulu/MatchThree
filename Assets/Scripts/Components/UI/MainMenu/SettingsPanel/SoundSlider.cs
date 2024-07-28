using System;
using Datas;
using Events;
using Installers;
using UnityEngine;
using Utils;
using Zenject;

namespace UI.MainMenu.SettingsPanel
{
    public class SoundSlider : UISlider
    {
        [Inject] private MainMenuEvents MainMenuEvents { get; set; }
        [Inject] private PlayerData PlayerData { get; set; }
        protected override void OnEnable()
        {
            base.OnEnable();
            _soundSlider.value = PlayerData.SoundVal;
            _musicSlider.value = PlayerData.MusicVal;
        }
    
        protected override void OnMusicValueChanged(float val)
        {
            MainMenuEvents.MusicValueChanged?.Invoke(val);
            _mixer.SetFloat("music", Mathf.Log10(val) * 20);
        }
        
        protected override void OnSoundValueChanged(float val)
        {
            MainMenuEvents.SoundValueChanged?.Invoke(val);
            _mixer.SetFloat("sound", Mathf.Log10(val) * 20);
        }
    }
}