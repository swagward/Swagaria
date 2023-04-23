using UnityEngine;

namespace TerrariaClone.Runtime.Player
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance = null;
        public static bool Initialized { get; set; }
        public static bool BossActive { get; set; }
        
        private void Awake()
        {
            PauseControl.IsPaused = false;
            Time.timeScale = 1;
            
            //Debug.Log($"{PauseControl.IsPaused} : {Time.timeScale}.");
            
            DontDestroyOnLoad(this);
            
            if (Instance is null)
                Instance = this;
            else Destroy(gameObject);
        }

        // private void Update()
        // {
        //     Debug.Log($"{Initialized}");
        // }
    }
}