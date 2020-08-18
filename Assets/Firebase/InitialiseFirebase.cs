using Firebase.Analytics;
using UnityEngine;

namespace Firebase
{
    public class InitialiseFirebase : MonoBehaviour
    {
        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            });
        }
    }
}
