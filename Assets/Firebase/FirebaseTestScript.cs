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
            FireBaseManager.OnOpen();
        }

        private void Update()
        {
            if (Input.touches.Length > 0)
            {
                FireBaseManager.OnOpen();
            }
        }

        public void PurchaseEvent()
        {
            FireBaseManager.PurchaseEvent("plumbus", 69, "floogles");
        }
        
        public void GameStart()
        {
            FireBaseManager.GameStartEvent();
        }
        
        public void GameEnd()
        {
            FireBaseManager.GameEndEvent();
        }
    }
}
