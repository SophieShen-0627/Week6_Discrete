using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Rigidbody2D[] rbs ;

    private void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody2D>();

        foreach (var rb in rbs)
        {
            rb.gameObject.SetActive(false);
        }
    }
    public void DoExplosion()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].gameObject.SetActive(true);
            Vector2 forceDir = (rbs[i].transform.position - transform.position);
            float random = Random.Range(DataManager.datas.ExplosionForce * 0.5f, DataManager.datas.ExplosionForce * 1.5f);

            rbs[i].AddForce(forceDir * random, ForceMode2D.Impulse);
        }
    }
}
