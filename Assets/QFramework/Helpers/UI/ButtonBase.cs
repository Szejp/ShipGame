using System;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Helpers.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonBase : UIComponent
    {
        public Action OnButtonClicked;
        
        [SerializeField] Button button;

        Button Button
        {
            get
            {
                if (button == null)
                    button = GetComponent<Button>();

                return button;
            }
        }

        protected virtual void Awake()
        {
            base.Awake();
            Button.onClick.AddListener(OnButtonClickedHandler);
        }

        protected virtual void OnDestroy()
        {
            Button.onClick.RemoveListener(OnButtonClickedHandler);
        }

        protected virtual void OnButtonClickedHandler()
        {
            OnButtonClicked?.Invoke();
        }
    }
}