using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace QFramework.Helpers.ToolbarExtender.Editor
{
    public static class EditorScenesUtils
    {
        static string FirstScenePath => SceneManager.sceneCount > 0 ? EditorBuildSettings.scenes[0].path : null;

        public static void StartScene(string path, bool start = false)
        {
            if (path != null && EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(path);
                if (start)
                    EditorApplication.isPlaying = true;
            }
        }

        public static bool IsSceneLoaded(string path)
        {
            return path.Equals(SceneManager.GetActiveScene().path);
        }

        public static void StartFirstScene()
        {
            StartScene(FirstScenePath, true);
        }
    }
}