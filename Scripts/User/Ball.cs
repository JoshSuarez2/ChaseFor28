using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]private float speed;
    private bool impact;
    private float direction;
    private BoxCollider2D box;
    private Animator anim;
    private float duration;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check if projectile hit something
        if (impact) 
            return;
        float movementSpeed = speed * Time.deltaTime * direction;
        //Move object on the x-axis
        transform.Translate(movementSpeed, 0, 0);
        //The duration of the projectiles will not last forever if they do not make impact with anything
        duration += Time.deltaTime;
        if (duration > 7)
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When projectile makes impact with an object
        impact = true;
        box.enabled = false;
        anim.SetTrigger("detonate");

        if (collision.tag == "Enemy")//If hits enemy do 1 damage
            collision.GetComponent<Health>().DamageTaken(1);
    }
    public void SetDirection(float _direction)
    {
        //Tells projectile to fly either left or right
        direction = _direction;
        gameObject.SetActive(true);
        impact = false;
        box.enabled = true;
        duration = 0;

        float localScaleX = transform.localScale.x;
        //Check if the sign of localScaleX is not equal to the direction meaning fireball is facing wrong way
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }
    private void Deactivate()
    {
        //Helps deactivates the projectile after it is finished
        gameObject.SetActive(false);
    }
}
