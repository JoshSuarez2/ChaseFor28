using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]private float dmg;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //if the enemy has collided specifically with player
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().DamageTaken(dmg);
        }
    }
}
