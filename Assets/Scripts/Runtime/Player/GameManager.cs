using UnityEngine;

namespace TerrariaClone.Runtime.Player
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance = null;
        public static bool Initialized { get; set; }
        public static bool BossActive { get; set; }
        public static bool ParallaxShowing { get; set; } = false;

        private void Start()
        {
            PauseControl.IsPaused = false;
            Time.timeScale = 1;
            
            DontDestroyOnLoad(this);
            if (Instance is null)
                Instance = this;
            else Destroy(this.gameObject);
        }
    }
}