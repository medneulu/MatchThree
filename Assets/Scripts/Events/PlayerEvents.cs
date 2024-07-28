using UnityEngine.Events;

namespace Events
{
    public class PlayerEvents
    {
        public UnityAction PlayerScoreChanged;
        public UnityAction<bool, float> PlayerTimerStoped;
        public UnityAction LevelComplete;
        public UnityAction TimerOut;
    }
}