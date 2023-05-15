using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigSpike : EnemyDamage
{
    //When the big spike detects the player store the position in destination
    private Vector3 destination;
    //How fast and far spike travels
    [SerializeField]private float speed;
    [SerializeField] private float range;
    [SerializeField] private float Delay;
    [SerializeField] private LayerMask playerLay;
    private float Timer;
    //We can tell when the spike is attacking the player
    private bool attacking;
    private Vector3[] direct = new Vector3[4];
    private Vector3 originalPos;
    [SerializeField] private AudioClip collisionSound;

    //Gets called every time the object gets activated
    private void OnEnable()
    {
        //Starts in an idle position
        Stop();
    }
    private void Update()
    {
        if (attacking)
        {
            //Move the big spike towards the destination
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            Timer += Time.deltaTime;
            if(Timer > Delay) { SearchPlayer(); }
        }
    }
    private void SearchPlayer()
    {
        Directions();

        //Use for loop to search for player
        for(int i = 0; i < direct.Length; i++)
        {
            Debug.DrawRay(transform.position, direct[i], Color.red);
            //Detect player
            RaycastHit2D collide = Physics2D.Raycast(transform.position, direct[i], range, playerLay);
            //Spike found player and is currently not attacking then go to player and attack
            if(collide.collider != null && !attacking)
            {
                attacking = true;
                destination = direct[i];
                Timer = 0;
            }
        }
    }

    private void Directions()
    {
        //Move x axis
        direct[0] = -transform.up * range;
    }

    
    private void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }
    private void Restart()
    {
        gameObject.transform.position = originalPos;
    }
    private void Stop()
    {
        //Don't move if already at destination
        destination = transform.position;
        attacking = false;
        Invoke("Restart", 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(collisionSound);
        base.OnTriggerEnter2D(collision);
        //Stop when collides with something
        Stop();
    }
}
