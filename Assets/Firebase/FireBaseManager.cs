using Firebase.Analytics;
using UnityEngine;

namespace Firebase
{
    public static class FireBaseManager
    {
        private static FirebaseApp app; //Usages?
        private static bool fireBaseActive = false;
    
        public static void InitialiseFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                
                if (dependencyStatus == DependencyStatus.Available) 
                {
                    Debug.Log($"Firebase active");
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    
                    app = FirebaseApp.DefaultInstance;
                    
                    fireBaseActive = true;
                    
                } 
                else 
                {
                    // Firebase Unity SDK is not safe to use here.
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}. Firebase inactive");
                    app = null;
                    fireBaseActive = false;
                }
            });
        }

        // Update is called once per frame
        public static void SetPlayerIdentification()
        {
            if (!fireBaseActive) return;
            
            FirebaseAnalytics.SetUserId("test");
            FirebaseAnalytics.SetUserProperty("deviceId", SystemInfo.deviceUniqueIdentifier);
        }
    }
}
