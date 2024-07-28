using System;
using Datas;
using Events;
using UnityEngine;
using TMPro;
using UI.OnGame;
using Zenject;

public class PlayerTimerTMP : MonoBehaviour
{
    [Inject] private PlayerEvents PlayerEvents { get; set; }
    [Inject] private PlayerData PlayerData { get; set; }

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField]private float remainingTime;
    private bool _isTimeOut;

    public float RemainingTime
    {
        get => remainingTime;
    }
    private bool _isPaused;

    private void Update()
    {
        if (_isPaused == true)
        {
            return;
        }
        if (PlayerData.CurrTime > 0)
        {
            PlayerData.CurrTime -= Time.deltaTime;   
        }
        else if (PlayerData.CurrTime < 0)
        {
            if (_isTimeOut==false)
            {
                _isTimeOut = true;
                PlayerEvents.TimerOut?.Invoke();
            }
            PlayerData.CurrTime = 0;
        }
        
        int minutes = Mathf.FloorToInt(PlayerData.CurrTime / 60);
        int seconds = Mathf.FloorToInt(PlayerData.CurrTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }

    private void OnEnable()
    {
        PlayerEvents.PlayerTimerStoped += OnTimerStopped;
    }

    private void OnTimerStopped(bool _isPause, float _time)
    {
        _isPaused = _isPause;
    }

    private void OnDisable()
    {
        PlayerEvents.PlayerTimerStoped -= OnTimerStopped;
    }

    /*public void Pause()
     {
         Pause.SetActive(true);
         Time.timeScale = 0;
     }*/
}
