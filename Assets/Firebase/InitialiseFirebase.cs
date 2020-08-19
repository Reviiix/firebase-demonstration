using System;
using Firebase.Analytics;
using UnityEngine;

namespace Firebase
{
    public class InitialiseFirebase : MonoBehaviour
    {
        public FirebaseApp app;

        public static bool fireBaseActive = false;
        
        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                
                if (dependencyStatus == DependencyStatus.Available) 
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    
                    app = FirebaseApp.DefaultInstance;
                    
                    fireBaseActive = true;
                    
                    Camera.main.backgroundColor = Color.blue;
                } 
                else 
                {
                    // Firebase Unity SDK is not safe to use here.
                    UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    app = null;
                    fireBaseActive = false;
                    Camera.main.backgroundColor = Color.red;
                }
            });
        }

        private void Update()
        {
            if (Input.touches.Length > 0)
            {
                if (fireBaseActive)
                {
                    FirebaseAnalytics.SetUserId("test");
                    FirebaseAnalytics.SetUserProperty("deviceId", SystemInfo.deviceUniqueIdentifier);
                    Camera.main.backgroundColor = Color.green;
                    return;
                }
                Camera.main.backgroundColor = Color.red;
            }
        }
    }
}
