using TerrariaClone.Runtime.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TerrariaClone.Runtime.Player
{
    public class HealthManager : MonoBehaviour
    {
        public int currentHealth;
        public HealthInfo health;
        public TMP_Text textDisplay;
        public Slider healthSlider;

        private void Start()
        {
            currentHealth = health.defaultHealth;
        }

        private void Update()
        {
            textDisplay.text = $"{currentHealth} / {health.currentMaxHealth}";
            currentHealth = Mathf.Clamp(currentHealth, health.minHealth, health.currentMaxHealth);
        }

        public void AddHealth(int healthToAdd)
        {
            currentHealth += healthToAdd;
        }

        public void IncreaseTotalHealth(int totalIncrease)
        {
            health.currentMaxHealth += totalIncrease;
        }
    }
}