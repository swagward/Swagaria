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
        public TMP_Text textDisplay;

        [Header("Health Info")]
        public int defaultHealth = 100;
        public int currentMaxHealth = 100;
        public int defaultMaxHealth = 400;

        private void Start()
        {
            UpdateHealth();
        }

        public void AddHealth(int healthToAdd)
        {
            currentHealth += healthToAdd;
            UpdateHealth();
        }

        public void IncreaseTotalHealth(int totalIncrease)
        {
            currentMaxHealth += totalIncrease;
            UpdateHealth();
        }

        public void TakeDamage(int remove, PlayerController player)
        {
            currentHealth -= remove;
            UpdateHealth();

            StartCoroutine(iFrames(player));
        }

        /// <summary>
        /// https://www.youtube.com/watch?v=YSzmCf_L2cE&ab_channel=Pandemonium
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private IEnumerator iFrames(PlayerController player)
        {
            Debug.Log(player);
            //8 - player, 9 - enemy
            Physics.IgnoreLayerCollision(8, 9, true);

            for (int i = 0; i < player.flashCount; i++)
            {
                foreach (var playerSprite in player.playerSprite)
                {
                    var spriteMaterial = playerSprite.material;

                    spriteMaterial.SetColor("_EmissionColor", new Color(1, 0, 0, .5f)); //red
                    //spriteMaterial.color = new Color(1, 0, 0, .5f);
                    yield return new WaitForSeconds(player.iFrameDuration / (player.flashCount * 2));
                    spriteMaterial.SetColor("_EmissionColor", Color.white);
                    //spriteMaterial.color = Color.white;
                    yield return new WaitForSeconds(player.iFrameDuration / (player.flashCount * 2));
                }
            }
            Physics.IgnoreLayerCollision(8, 9, false);

        }

        private void UpdateHealth()
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, currentMaxHealth);
            textDisplay.text = $"{currentHealth} / {currentMaxHealth}";
        }
        //player.iFrameDuration / (player.flashCount * 2)
    }
}