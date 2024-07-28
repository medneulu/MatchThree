using Events;
using UnityEngine;
using Utils;
using Zenject;

public class ExitBTN : UIBTN
{
   private AudioManager _audioManager;

   [Inject] private GameMenuEvents GameMenuEvents { get; set; }
   private void Awake()
   {
      _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
   }

   protected override void OnClick()
   {
      GameMenuEvents.ExitBTN?.Invoke();
   }
}
