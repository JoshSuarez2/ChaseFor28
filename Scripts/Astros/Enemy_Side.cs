using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Side : MonoBehaviour
{
    [SerializeField] private float damage;
    //Move the saw and speed 
    [SerializeField] private float moveDist;
    [SerializeField] private float speed;
    private bool LeftMove;
    private float leftEnd;
    private float rightEnd;

    private void Awake()
    {
        //Range of movement
        leftEnd = transform.position.x - moveDist;
        rightEnd = transform.position.x + moveDist;
    }

    private void Update()
    {
        //Check if moving left or right
        if (LeftMove)
        {
            //Has not reached the edge on the left yet
            if (transform.position.x > leftEnd)
            {
                //Move position
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            //Has reached the edge so can't move no more
            else
            {
                LeftMove = false;
            }
        }
        else
        {
            if (transform.position.x < rightEnd)
            {
                //Move position
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                LeftMove = true;
            }
        }

        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().DamageTaken(damage);
        }
    }
}
