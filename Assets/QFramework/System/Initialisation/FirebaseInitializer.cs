using System;
using Firebase;
using QFramework.Modules.QConfig.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QFramework.System.Initialisation
{
    public class FirebaseInitializer : MonoBehaviour
    {
        public static Action OnInitalised;

        void Start()
        {
            InitializeFirebaseAndStart();
        }

        public void InitializeFirebaseAndStart()
        {
            var dependencyStatus = FirebaseApp.CheckDependencies();

            if (dependencyStatus != DependencyStatus.Available)
                FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
                {
                    dependencyStatus = FirebaseApp.CheckDependencies();
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        InitializeFirebaseComponents();
                    }
                    else
                    {
                        Debug.LogError(
                            "Could not resolve all Firebase dependencies: " + dependencyStatus);
                        Application.Quit();
                    }
                });
            else
                InitializeFirebaseComponents();
        }

        void InitializeFirebaseComponents()
        {
            Configs.FetchRemoteConfig().ContinueWith(task => FinishFirebaseInitialisation());
        }

        void FinishFirebaseInitialisation()
        {
            ThreadDispatcher.Dispatch(() =>
            {
                Configs.RefreshConfigsData();
                OnInitalised?.Invoke();
                SceneManager.LoadScene(1);
                Debug.Log("[FirebaseInitialiser] Initialised");
            });
        }
    }
}