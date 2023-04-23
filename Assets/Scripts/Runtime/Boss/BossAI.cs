using System;
using TerrariaClone.Runtime.Player;
using UnityEngine;

namespace TerrariaClone.Runtime.Boss
{
    public class BossAI : MonoBehaviour
    {
        public BossStates states;
        
        private void Start()
        {
            GameManager.BossActive = true;
        }

        private void Update()
        {
            switch (states)
            {
                case BossStates.Chasing:
                {
                    //boss follow player
                    break;
                }
                case BossStates.Idling:
                {
                    //get away from player
                    break;
                }
                case BossStates.Attacking:
                {
                    
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Death()
        {
            Destroy(this.gameObject);
            GameManager.BossActive = false;
        }
    }

    public enum BossStates
    {
        Chasing,
        Attacking,
        Idling
    }
}
