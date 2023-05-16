using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody2D judge;
    //Let's me edit it directly from unity 
    [SerializeField]private float speed;
    private Animator anim;
    private BoxCollider2D box;
    [SerializeField]private LayerMask groundLay;
    [SerializeField] private AudioClip jumpNoise;
    

    //Every time you start the game the script is being loaded and awake is executed
    private void Awake()
    {
        //Getting access to the rigid body, animator and box collider
        judge = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        judge.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, judge.velocity.y);

        //Points the direction to where the player is moving
        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (Input.GetAxis("Horizontal") < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1 ,1);
        }
        //Boolean variable that returns true when key is pressed and false when not 
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }

        //Animator parameters
        anim.SetBool("run", Input.GetAxis("Horizontal") !=0);
        anim.SetBool("grounded", isGrounded());


    }

    private void Jump()
    {
        if (isGrounded())
        {
            SoundManager.instance.PlaySound(jumpNoise);
            judge.velocity = new Vector2(judge.velocity.x, speed);
            anim.SetTrigger("jump");
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D Hit = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLay);
        return Hit.collider !=null;
    }

    public bool ableToAttack()
    {
        return Input.GetAxis("Horizontal") == 0 && isGrounded();
    }
}
