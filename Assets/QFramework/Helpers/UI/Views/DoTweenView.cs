using DG.Tweening;
using ExternalTools.Demigiant.DOTweenPro;

namespace QFramework.Helpers.UI.Views
{
    public class DoTweenView : View
    {
        public DOTweenAnimation animation;
        public bool forceRefresh;

        bool isShown;
        
        public override bool CanShow { get; }
        public override void Show()
        {
            if (!isShown || forceRefresh)
            {
                isShown = true;
                animation.DORestart();
            }
        }

        public override void Hide()
        {
            if (isShown)
            {
                animation.DOComplete();
                animation.DOPlayBackwards();
                isShown = false;
            }
        }
    }
}