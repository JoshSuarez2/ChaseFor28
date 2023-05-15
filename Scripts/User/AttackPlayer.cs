using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private Animator anim;
    private MovePlayer movePlayer;
    //How much time can pass before we can use our next shot
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform ProjectilePoint;
    [SerializeField] private GameObject[] projectiles;
    //When game starts timer will automatically be on 0 meaning player can't attack hence the big number to combat this
    private float timer = Mathf.Infinity;
    [SerializeField] private AudioClip fireballSound; // sound effect

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movePlayer = GetComponent<MovePlayer>();
       
    }

    private void Update()
    {
        //Left clicked mouse and enough time has passed
        if(Input.GetMouseButton(0) && timer > attackCooldown && movePlayer.ableToAttack()) 
        {
            Attack();
        }
        // Increment it on every frame
        timer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);// fireball noise
        anim.SetTrigger("attack");
        // When we attack, the cooldown timer is reset to 0
        timer = 0;
        projectiles[FindProjectile()].transform.position = ProjectilePoint.position;
        projectiles[FindProjectile()].GetComponent<Ball>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindProjectile()
    {
        for (int i = 0; i<projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy) return i;
        }
        
        
        return 0;
    }
}
