using System;
using UnityEngine;

namespace QFramework.Helpers.Editor.GUIUtils
{
    public class GUIEnabled : IDisposable
    {
        readonly bool prevValue;

        public GUIEnabled(bool enabled)
        {
            prevValue = GUI.enabled;
            GUI.enabled = enabled;
        }

        public void Dispose()
        {
            GUI.enabled = prevValue;
        }
    }
}