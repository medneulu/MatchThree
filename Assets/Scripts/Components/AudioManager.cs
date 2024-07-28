using System;
using Events;
using Extensions.Unity;
using UnityEngine;
using Utils;
using Zenject;

public class AudioManager : EventListenerMono
{
    [Inject] private MainUIEvents MainUIEvents { get; set; }
    [Inject] private GridEvents GridEvents { get; set; }
    [Header("Audio Source")]
    
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip tileSound;
    public AudioClip buttonClick;
    private static AudioManager audioManager;
    
    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log(transform.name + "is duplicate.");
            gameObject.Destroy();
        }
    }

    private void Start()
    {
        _musicSource.clip = background;
        _musicSource.Play();
    }
    
    protected override void RegisterEvents()
    {
        GridEvents.TileDestroyed += OnTileDestroyed;
        MainUIEvents.ButtonClick += OnButtonClick;
    }

    private void OnTileDestroyed()
    {
        _sfxSource.PlayOneShot(tileSound);
    }

    private void OnButtonClick()
    {
        _sfxSource.PlayOneShot(buttonClick);
    }

    protected override void UnRegisterEvents()
    {
        GridEvents.TileDestroyed -= OnTileDestroyed;
        MainUIEvents.ButtonClick -= OnButtonClick;
    }
}
