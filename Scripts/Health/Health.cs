using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    //How much health you start with
    [SerializeField] private float startHealth;
    //How much health you currently have
    public float currHealth { get; private set; } //can only change this value from this script
    private Animator anim;
    private bool died;
    [SerializeField] private float invulnerability;
    //Time before turning vulnerable again
    [SerializeField] private int numFlashes;
    private SpriteRenderer spriteRend;
    [SerializeField] private AudioClip deathNoise;//sound when dead
    [SerializeField] private AudioClip damageNoise;//sound when damaged

    private void Awake()
    {
        currHealth = startHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void DamageTaken(float _damage)
    {
        //Make sure health does not go below zero or start health value / Subtract damage from current health 
        currHealth = Mathf.Clamp(currHealth - _damage, 0, startHealth);
        
        if (currHealth>0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invul());
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
    private IEnumerator Invul()
    {
        //This will make the player not be effected by collisions with objects
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulnerability / (numFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(invulnerability / (numFlashes * 2));
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
        currHealth = Mathf.Clamp(currHealth + val, 0, startHealth);
    }

    public void judgeRespawn()
    {
        died = false;
        AddHealth(startHealth);
        //Get player out of the death animation
        anim.ResetTrigger("die");
        anim.Play("Idle");
        //Enable moving and attacking again after disabling in damageTaken method
        GetComponent<MovePlayer>().enabled = true;
        GetComponent<MeleeAstros>().enabled = true;
    }

    
}
