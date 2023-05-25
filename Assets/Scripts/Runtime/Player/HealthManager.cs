using System.Collections;
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

        public void TakeDamage(int remove, PlayerController player)
        {
            currentHealth -= remove;
            StartCoroutine(iFrames(player));
        }

        /// <summary>
        /// https://www.youtube.com/watch?v=YSzmCf_L2cE&ab_channel=Pandemonium
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private IEnumerator iFrames(PlayerController player)
        {
            //8 - player, 9 - enemy
            Physics.IgnoreLayerCollision(8, 9, true);

            for (int i = 0; i < player.flashCount; i++)
            {
                player.playerSprite[i].color = new Color(1, 0, 0, .5f);
                yield return new WaitForSeconds(player.iFrameDuration / (player.flashCount * 2));
                player.playerSprite[i].color = Color.white;
                yield return new WaitForSeconds(player.iFrameDuration / (player.flashCount * 2));
                Debug.Log($"iterations {i}");

            }
            Physics.IgnoreLayerCollision(8, 9, false);

        }
    }
}