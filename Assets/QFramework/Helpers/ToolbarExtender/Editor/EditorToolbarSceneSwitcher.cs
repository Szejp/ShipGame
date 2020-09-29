using System.Linq;
using QFramework.Helpers.Editor.GUIUtils;
using UnityEditor;
using UnityEngine;

namespace QFramework.Helpers.ToolbarExtender.Editor
{
    [InitializeOnLoad]
    internal static class EditorToolbarSceneSwitcher
    {
        static EditorToolbarSceneSwitcher()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            using (new GUIEnabled(EditorApplication.isPlaying == false))
            {
                ShowPlayButton();
                GUILayout.FlexibleSpace();
                ShowMenu();
            }
        }

        static void ShowPlayButton()
        {
            var tex = EditorGUIUtility.IconContent(@"PlayButton").image;

            using (new GUIColor(new Color(0.34f, 1f, 0.64f)))
            {
                var content = new GUIContent(tex, "Start First scene");
                var guiStyle = ToolbarStyles.Command;

                var rect = ToolbarStyles.GetThickArea(GUILayoutUtility.GetRect(content, guiStyle));
                if (GUI.Button(rect, content, guiStyle))
                {
                    EditorScenesUtils.StartFirstScene();
                }
            }
        }

        static void ShowMenu()
        {
            var content = new GUIContent("Build Scenes ");
            var guiStyle = ToolbarStyles.DropDown;
            var rect = ToolbarStyles.GetThinArea(GUILayoutUtility.GetRect(content, guiStyle));

            if (GUI.Button(rect, content, guiStyle))
            {
                var menu = new GenericMenu();

                foreach (var scene in EditorBuildSettings.scenes)
                {
                    var isLoaded = EditorScenesUtils.IsSceneLoaded(scene.path);
                    menu.AddItem(new GUIContent(GetNameFromScenePath(scene.path)), isLoaded, StartScene, scene.path);
                }

                menu.ShowAsContext();
            }
        }

        static void StartScene(object pathText)
        {
            EditorScenesUtils.StartScene(pathText as string);
        }

        static string GetNameFromScenePath(string path)
        {
            return path.Split('/').Last();
        }
    }
}