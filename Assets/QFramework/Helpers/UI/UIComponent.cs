using QFramework.Helpers.UI.Views;
using UnityEngine;

namespace QFramework.Helpers.UI
{
    public class UIComponent : MonoBehaviour
    {
        View view;

        public View View => view;

        public virtual void Awake()
        {
            view = GetComponent<View>();
        }

        public void Activate()
        {
            if(view)
                view.Show();
            else
                gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            if(view)
                view.Hide();
            else
                gameObject.SetActive(false);
        }
    }
}