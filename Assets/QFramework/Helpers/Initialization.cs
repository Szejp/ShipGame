using UnityEngine;
using UnityEngine.SceneManagement;

namespace QFramework.Helpers {
    public class Initialization : MonoBehaviour
    {
        public static void Init()
        {
            SceneManager.LoadScene(1);
        }
    }
}