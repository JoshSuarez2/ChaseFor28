using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{

    //How much health you start with
    [SerializeField] private float startHearts;
    //How much health you currently have
    public float currHearts;
    private Animator anim;
    private bool died;
    [SerializeField] private float invulnerability;
    //Time before turning vulnerable again
    [SerializeField] private int numInvul;
    private SpriteRenderer sprite;
    [SerializeField] private AudioClip damageNoise;//audio when damaged
    [SerializeField] private AudioClip deathNoise;//audio when dead
    

    private void Awake()
    {
        currHearts = startHearts;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void DamageTaken(float dmg)
    {
        //Make sure health does not go below zero or start health value / Subtract damage from current health 
        currHearts = Mathf.Clamp(currHearts - dmg, 0, startHearts);
        
        if (currHearts >0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Inv());
            SoundManager.instance.PlaySound(damageNoise);
        }
        else
        {
            if(!died)
            {
                anim.SetTrigger("die");
                //Player cannot move once dead
                if(GetComponent<MovePlayer>() != null)
                    GetComponent<MovePlayer>().enabled = false;
                //Enemy
                if(GetComponentInParent<AstrosPatrol>() != null)
                    GetComponentInParent<AstrosPatrol>().enabled = false;
                if (GetComponent<MeleeAstros>() != null)
                    GetComponent<MeleeAstros>().enabled = false;
                //Dying will not be repeated twice
                died = true;
                SoundManager.instance.PlaySound(deathNoise);
            }
            
        }
        
    }

    //Helps player not get instant killed by traps or enemies
    private IEnumerator Inv()
    {
        //This will make the player not be effected by collisions with objects
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numInvul; i++)
        {
            sprite.color = new Color(2, 0, 0, 0.8f);
            yield return new WaitForSeconds(invulnerability / (numInvul * 2));
            sprite.color = Color.white;
            yield return new WaitForSeconds(invulnerability / (numInvul * 2));
        }
        //Time of not being hurt 
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    //Remove leftover enemy
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void AddHealth(float val)
    {
        currHearts = Mathf.Clamp(currHearts + val, 0, startHearts);
    }

    public void judgeRespawn()
    {
        died = false;
        AddHealth(startHearts);
        //Get player out of the death animation
        anim.ResetTrigger("die");
        anim.Play("Idle");
        //Enable moving and attacking again after disabling in damageTaken method
        GetComponent<MovePlayer>().enabled = true;
        GetComponent<MeleeAstros>().enabled = true;
    }

    
}
