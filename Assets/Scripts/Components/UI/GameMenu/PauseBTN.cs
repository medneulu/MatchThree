using Events;
using Utils;
using UnityEngine;
using Zenject;

namespace UI.OnGame
{
    public class PauseBTN : UIBTN
    {
        [Inject] private GameMenuEvents GameMenuEvents { get; set; }
        [Inject] private AudioManager AudioManager { get; set; }
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        
        private void Awake()
        {
            AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
        protected override void OnClick()
        {
            GameMenuEvents.PauseBTN?.Invoke();
        }
        
    }
}