using System;
using TerrariaClone.Runtime.Data;
using TerrariaClone.Runtime.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TerrariaClone.Runtime.Boss
{
    public class BossAI : MonoBehaviour
    {
        public BossStates states;
        public BossSummon thisBoss;
        [SerializeField] private Vector2 goTo;
        private GameObject player;

        public void StartEvent()
        {
            GameManager.BossActive = true;
            player = FindObjectOfType<PlayerController>().gameObject;
            goTo = GetNewPosition();
        }

        private void Update()
        {
            if((Vector2)transform.position != goTo)
                transform.position = Vector2.MoveTowards(transform.position, goTo, thisBoss.moveSpeed * Time.deltaTime);
            else goTo = GetNewPosition();

            //rotation
            var angle = Mathf.Atan2(player.transform.position.y - this.transform.position.y, 
                                    player.transform.position.x - this.transform.position.x) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, thisBoss.rotateSpeed * Time.deltaTime);

            var summonChance = Random.Range(-300, 300);
            if(summonChance is 0)
            {
                Instantiate(thisBoss.miniBossSummon, this.transform.position, targetRotation);
            }

            
        }

        private Vector2 GetNewPosition()
        {
            var posX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            var posY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);

            return new Vector2(posX, posY);
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
