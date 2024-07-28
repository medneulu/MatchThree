using Events;
using UnityEngine;
using Zenject;
using Utils;

public class NextLevelBTN : UIBTN
{
    [Inject] private GameMenuEvents GameMenuEvents { get; set; }
    private AudioManager _audioManager;
    
    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    protected override void OnClick()
    {
        GameMenuEvents.NextLevelBTN?.Invoke();
    }
}
