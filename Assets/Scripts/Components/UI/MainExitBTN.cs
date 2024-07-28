using Events;
using Utils;
using Zenject;

namespace UI.Main
{
    public class MainExitBTN : UIBTN
    {
        [Inject] private MainUIEvents MainUIEvents { get; set; }
        protected override void OnClick()
        {
            MainUIEvents.ExitBTN?.Invoke();
        }
    }
}