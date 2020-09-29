#if !HUF_EDITOR_TOPBAR_OFF

using UnityEngine;

namespace QFramework.Helpers.ToolbarExtender.Editor 
{
    static class ToolbarStyles
    {
        public static GUIStyle Command
        {
            get { return (GUIStyle) "Command"; }
        }
        
        public static GUIStyle DropDown
        {
            get { return (GUIStyle) "DropDown"; }
        }
        
        public static Rect GetThinArea(Rect pos)
        {
            return new Rect(pos.x, 2f, pos.width, 18f);
        }

        public static Rect GetThickArea(Rect pos)
        {
            return new Rect(pos.x, 0f, pos.width, 24f);
        }
    }
}

#endif