using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    
    //How much time before the trap turns on after it has been stepped on
    [SerializeField]private float delay;
    //How long the trap stays on after it has been stepped on
    [SerializeField] private float time;
    private Animator anim;
    private SpriteRenderer sprite;
    [SerializeField] private float dmg;
    [SerializeField] private AudioClip fireNoise;
    //When the trap has been triggered or activated
    private bool on;
    private bool trig;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Check if trap has collided with player
        if(col.tag == "Player")
        {
            if(!trig)
            {
                //Coroutines are excellent when modeling behavior over several frames. Essential when working with IEnumerator
                StartCoroutine(FireOn());
            }
            if(on)
            {
                col.GetComponent<Health>().DamageTaken(dmg);
            }
        }

    }
    private IEnumerator FireOn()
    {
        trig = true;
        //Make the visual animation of fire incoming
        sprite.color = Color.red;
        //Pause between being stepped on and it turning on
        yield return new WaitForSeconds(delay);
        SoundManager.instance.PlaySound(fireNoise);
        //Back to normal before fire is ignited
        sprite.color = Color.white;
        //When this period is over the trap is activated
        on = true;
        anim.SetBool("activated", true);
        //Time trap is on
        yield return new WaitForSeconds(time);
        //Turn it all off
        on = false;
        trig = false;
        anim.SetBool("activated", false);
    }
}
