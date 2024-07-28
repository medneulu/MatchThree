using Events;
using Utils;
using Zenject;

namespace UI.OnGame
{
    public class ExitBTN : UIBTN
    {
        [Inject] private GameMenuEvents GameMenuEvents { get; set; }
        protected override void OnClick()
        {
            GameMenuEvents.ExitBTN?.Invoke();
        }
    }
}