using UnityEngine;

namespace Firebase
{
    public class FirebaseTestScript : MonoBehaviour
    {
        private void Awake()
        {
            FireBaseManager.InitialiseFirebase();
        }
        
        private void Start()
        {
            FireBaseManager.SetPlayerIdentification();
        }

        private void Update()
        {
            if (Input.touches.Length > 0)
            {
                FireBaseManager.SetPlayerIdentification();
            }
        }
    }
}
