using Events;
using UnityEngine;
using Utils;
using Zenject;

public class ResumeBTN : UIBTN
{
    private AudioManager _audioManager;
    [Inject] private GameMenuEvents GameMenuEvents { get; set; }
    
    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    protected override void OnClick()
    {
        GameMenuEvents.ResumeBTN?.Invoke();
    }
}
