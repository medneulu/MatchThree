using System;
using Datas;
using DG.Tweening;
using Events;
using Extensions.DoTween;
using Extensions.Unity.MonoHelper;
using UnityEngine;
using Zenject;

namespace Components.UI
{
    public class PlayerScoreTMP : UITMP, ITweenContainerBind
    {
        [Inject] private PlayerEvents PlayerEvents { get; set; }
        [Inject] private PlayerData PlayerData { get; set; }
        
        private Tween _counterTween;
        public ITweenContainer TweenContainer{get;set;}
        private int _currCounterVal;
        private int _playerScore;

        private void Awake()
        {
            TweenContainer = TweenContain.Install(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            RenderScore(PlayerData.CurrScore);
        }


        private void OnCounterUpdate(int val)
        {
            _currCounterVal = val;
            RenderScore(_currCounterVal);
        }

        private void RenderScore(int currCounterVal)
        {
            _myTMP.text = $"Score: {currCounterVal}";
        }

        private void WriteScore()
        {
            _playerScore = PlayerData.CurrScore;

            if(_counterTween.IsActive()) _counterTween.Kill();
            
            _counterTween = DOVirtual.Int
            (
                _currCounterVal,
                _playerScore,
                1f,
                OnCounterUpdate
            );

            TweenContainer.AddTween = _counterTween;
        }

        protected override void RegisterEvents()
        {
            PlayerEvents.PlayerScoreChanged += OnPlayerScoreChanged;
        }

        private void OnPlayerScoreChanged()
        {
            WriteScore();
        }

        protected override void UnRegisterEvents()
        {
            PlayerEvents.PlayerScoreChanged -= OnPlayerScoreChanged;
        }
    }
}