using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAstros : MonoBehaviour
{
    [SerializeField] private float atkWait;
    [SerializeField] private int dmg;
    private float Timer = Mathf.Infinity;//Let's enemy attack right away
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playLay;
    [SerializeField] private float range;//How far enemy sees
    [SerializeField] private float dist;
    private Animator anim;
    private Health playerHealth;
    private AstrosPatrol astrosPatrol;
    [SerializeField] private AudioClip swordNoise; //Attack noise

    private void Awake()
    {
        anim = GetComponent<Animator>();
        astrosPatrol = GetComponentInParent<AstrosPatrol>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if(PlayerFound())
        {
            if(Timer >= atkWait && playerHealth.currHearts > 0)
             {
                Timer = 0;//enemy just attacked
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(swordNoise);
             }

        }
        if (astrosPatrol != null)
            astrosPatrol.enabled = !PlayerFound();//Patrol when player is not in sight, stop patrolling when found      
    }
    //Enemy spots player
    private bool PlayerFound()
    {
        //We can tell if the enemy sees player
        //transform.localScale.x to change directions
        RaycastHit2D rayHit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x * dist,new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z), 0, Vector2.left, 0, playLay);
        if(rayHit.collider!=null)
        {
            playerHealth = rayHit.transform.GetComponent<Health>();
        }
        return rayHit.collider != null;
    }
    //The area which user is spotted
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * dist, new Vector3(box.bounds.size.x * range, box.bounds.size.y, box.bounds.size.z));
    }

    private void DmgPlayer()
    {
        if (PlayerFound())//Hurt player if found
        {
            playerHealth.DamageTaken(dmg);
        }
    }
}
