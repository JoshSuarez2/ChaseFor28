using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    //Store last checkpoint in here
    private Transform currCheckpoint;
    //Reset player's health when you respawn
    private Health judgeHealth;
    //Audio when checkpoint is reached
    [SerializeField] private AudioClip checkpointAudio;

    private void Awake()
    {
        judgeHealth = GetComponent<Health>();
    }

    public void judgeRespawn()
    {
        //Transfer player to checkpoint flag
        transform.position = currCheckpoint.position;
        //Replenish health and animation
        judgeHealth.judgeRespawn();
        
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Checkpoint")
        {
            currCheckpoint = col.transform;//Saving spot in checkpoint
            SoundManager.instance.PlaySound(checkpointAudio);
            col.GetComponent<Collider2D>().enabled = false; // Disable this collision so it doesn't keep happening
            //Visual of flag 
            col.GetComponent<Animator>().SetTrigger("flag");

        }
    }
}
