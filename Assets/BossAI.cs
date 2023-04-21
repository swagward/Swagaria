using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private void Start()
    {
        GameManager.BossActive = true;

        StartCoroutine(OnDeath());
    }

    private IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(3);
        GameManager.BossActive = false;
    }
}
