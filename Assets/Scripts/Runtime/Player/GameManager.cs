using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelWorlds.Runtime.Player
{
    public class GameManager : MonoBehaviour
    {
        public static bool IsPaused { get; private set; }
        [SerializeField] private List<GameObject> hideOnPause;
        [SerializeField] private List<GameObject> showOnPause;
        
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            
            IsPaused = !IsPaused;
            SwitchState();
        }

        private void SwitchState()
        {
            switch (IsPaused)
            {
                case true:
                {
                    //Game is paused
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;

                    foreach (var obj in hideOnPause)
                        obj.SetActive(false);
                    foreach (var obj in showOnPause)
                        obj.SetActive(true);
                    break;
                }
                case false:
                {
                    //Game is running
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Confined;

                    foreach (var obj in hideOnPause)
                        obj.SetActive(true);
                    foreach (var obj in showOnPause)
                        obj.SetActive(false);
                    break;
                }
            }
        }
    }
}
