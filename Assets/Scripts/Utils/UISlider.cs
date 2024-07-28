using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Utils
{
    public abstract class UISlider : EventListenerMono
    {
        [SerializeField] protected Slider _soundSlider;
        [SerializeField] protected Slider _musicSlider;
        [SerializeField] protected AudioMixer _mixer;

        protected override void RegisterEvents()
        {
            _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
            _soundSlider.onValueChanged.AddListener(OnSoundValueChanged);
        }

        protected abstract void OnMusicValueChanged(float val);
        protected abstract void OnSoundValueChanged(float val);

        protected override void UnRegisterEvents()
        {
            _musicSlider.onValueChanged.RemoveListener(OnMusicValueChanged);
            _soundSlider.onValueChanged.RemoveListener(OnSoundValueChanged);
        }
    }
}