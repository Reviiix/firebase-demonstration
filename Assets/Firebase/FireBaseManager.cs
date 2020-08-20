using Firebase.Analytics;
using UnityEngine;

namespace Firebase
{
    public static class FireBaseManager
    {
        private static bool _firstTimePlaying = false;
        private static FirebaseApp app; //Usages?
        private static bool fireBaseActive = false;

        private const string gamesStartedID = "amountOfGamesStarted";
        private const string gamesLostID = "amountOfGameOvers";
        private const string gamesWonID = "amountOfGameOvers";
    
        public static void InitialiseFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                
                if (dependencyStatus == DependencyStatus.Available) 
                {
                    Debug.Log($"Firebase active.");
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    
                    app = FirebaseApp.DefaultInstance;
                    
                    fireBaseActive = true;
                } 
                else 
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}. Firebase inactive");
                    app = null;
                    fireBaseActive = false;
                }
            });
        }
        
        public static void OnOpen()
        {
            if (!_firstTimePlaying) return;
            
            _firstTimePlaying = true;
            
            SetDeviceIdentification();
            SetFirstOpen();
        }

        private static void SetDeviceIdentification()
        {
            if (!fireBaseActive) return;
            
            FirebaseAnalytics.SetUserProperty("deviceId", SystemInfo.deviceUniqueIdentifier);
        }

        public static void SetFirstOpen()
        {
            if (!fireBaseActive) return;

            FirebaseAnalytics.SetUserProperty("firstOpen", System.DateTime.Now.ToString());
        }
        
        //Potentially do some checks backed by player prefs to ensure game start cant be called again before game end and game end cant be called before game start.
        //See how game pans out.
        public static void GameStartEvent()
        {
            if (!fireBaseActive) return;
            
            FirebaseAnalytics.LogEvent("gameStarted", "time",System.DateTime.Now.ToString());
            
            FirebaseAnalytics.SetUserProperty("inGame", true.ToString());
        }
        
        //Potentially do some checks backed by player prefs to ensure game start cant be called again before game end and game end cant be called before game start.
        //See how game pans out.
        public static void GameEndEvent()
        {
            if (!fireBaseActive) return;
            
            FirebaseAnalytics.LogEvent("gameEnded", "time",System.DateTime.Now.ToString());
            
            FirebaseAnalytics.SetUserProperty("inGame", false.ToString());
        }

        public static void PurchaseEvent(string itemName, int value, string virtualCurrencyName)
        {
            var purchaseParameter = new []
            {
                new Parameter(FirebaseAnalytics.ParameterItemName, itemName),
                new Parameter(FirebaseAnalytics.ParameterValue, value),
                new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, virtualCurrencyName)
            };

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSpendVirtualCurrency, purchaseParameter);
        }
    }
}
