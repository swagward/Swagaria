using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TerrariaClone.Runtime.Data
{
    [CreateAssetMenu(fileName = "newHealthSetting", menuName = "Pixel Worlds/Health Info")]
    public class HealthInfo : ScriptableObject
    {
        public int minHealth = 0;

        public int defaultHealth;
        public int currentMaxHealth;
        public int defaultMaxHealth;

        private void Reset()
        {
            defaultHealth = 0;
            currentMaxHealth = 100;
            defaultMaxHealth = 400;
        }
    }
}
