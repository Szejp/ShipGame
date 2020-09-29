namespace QFramework.Helpers.UI.Views
{
    public class GameObjectView : View
    {
        public override bool CanShow => true;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}