using UnityEngine.SceneManagement;

namespace QFramework.Helpers
{
    public static class SceneHelper
    {
        public static void ReloadActiveScene()
        {
            var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.UnloadSceneAsync(activeSceneIndex);
            SceneManager.LoadSceneAsync(activeSceneIndex);
        }
    }
}