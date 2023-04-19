using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelWorlds.Runtime.Player
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance = null;
        
        private void Awake()
        {
            PauseControl.IsPaused = false;
            Time.timeScale = 1;
            
            Debug.Log($"{PauseControl.IsPaused} : {Time.timeScale}.");
            
            DontDestroyOnLoad(this);
            
            if (Instance is null)
                Instance = this;
            else Destroy(gameObject);
        }
    }
}