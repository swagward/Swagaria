using System.Collections.Generic;
using UnityEngine;

namespace TerrariaClone.Runtime.Player
{
    public class PauseControl : MonoBehaviour
    {
        public static bool IsPaused { get; set; }
        [SerializeField] private List<GameObject> hideOnPause;
        [SerializeField] private List<GameObject> showOnPause;
        
        
        private void Update()
        {
            if (!GameManager.Initialized) return;
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