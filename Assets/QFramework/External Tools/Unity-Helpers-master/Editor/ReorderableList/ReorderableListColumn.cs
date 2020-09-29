using UnityEngine;

namespace QFramework.External_Tools.Editor.ReorderableList
{
    public class ReorderableListColumn<T>
    {
        public int Width { get; set; }
        public float WidthRatio { get; set; }
        public string Name { get; set; }
        public ReorderableListItemRendererDelegate<T> ItemRenderer { get; set; }

        public ReorderableListColumn()
        {
            Width = -1;
            WidthRatio = 1;
            Name = "";
        }
    }

    public delegate void ReorderableListItemRendererDelegate<T>(T obj, Rect rect, int index, bool isActive, bool isFocused);
}
