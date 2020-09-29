using System;
using UnityEngine;

namespace QFramework.Helpers.Editor.GUIUtils
{
    public class GUIColor : IDisposable
    {
        readonly Color prevColor;

        public GUIColor(Color color)
        {
            prevColor = GUI.color;
            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = prevColor;
        }
    }
}