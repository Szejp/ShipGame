using UnityEngine;

namespace QFramework.Helpers.UI.Views
{
    public abstract class View : MonoBehaviour
    {
        public abstract bool CanShow { get; }
        public abstract void Show();
        public abstract void Hide();

        public void TryShow()
        {
            if(CanShow)
                Show();
        }
    }
}