using System;
using TerrariaClone.Runtime.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TerrariaClone.Runtime.Boss
{
    public class BossAI : MonoBehaviour
    {
        public BossStates states;

        public void StartEvent()
        {
            GameManager.BossActive = true;
        }

        private void Update()
        {
            
        }

        private void Death()
        {
            Destroy(this.gameObject);
            GameManager.BossActive = false;
        }
    }

    public enum BossStates
    {

    }
}
