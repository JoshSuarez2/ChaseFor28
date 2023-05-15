using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Fire Timer")]
    //How much time before the trap turns on after it has been stepped on
    [SerializeField]private float activeDelay;
    //How long the trap stays on after it has been stepped on
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;
    [SerializeField] private float dmg;
    [SerializeField] private AudioClip fireNoise;

    //When the trap has been triggered or activated
    private bool active;
    private bool trig;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if trap has collided with player
        if(collision.tag == "Player")
        {
            if(!trig)
            {
                //Coroutines are excellent when modeling behavior over several frames. Essential when working with IEnumerator
                StartCoroutine(ActivateFire());
            }
            if(active)
            {
                collision.GetComponent<Health>().DamageTaken(dmg);
            }
        }

    }
    private IEnumerator ActivateFire()
    {
        trig = true;
        //Make the visual animation of fire incoming
        spriteRend.color = Color.red;
        //Pause between being stepped on and it turning on
        yield return new WaitForSeconds(activeDelay);
        SoundManager.instance.PlaySound(fireNoise);
        //Back to normal before fire is ignited
        spriteRend.color = Color.white;
        //When this period is over the trap is activated
        active = true;
        anim.SetBool("activated", true);
        //Time trap is on
        yield return new WaitForSeconds(activeTime);
        //Turn it all off
        active = false;
        trig = false;
        anim.SetBool("activated", false);
    }
}
