using UnityEditor;

namespace QFramework.Helpers.PlayerPrefs.Editor
{
    public class PlayerPrefsClear : UnityEditor.Editor
    {
        [MenuItem("PlayerPrefsClear/Clear")]
        public static void Clear()
        {
            UnityEngine.PlayerPrefs.DeleteAll();
        }
    }
}