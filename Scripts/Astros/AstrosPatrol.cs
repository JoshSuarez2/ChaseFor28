using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstrosPatrol : MonoBehaviour
{
    [SerializeField] private Transform rightEnd;
    [SerializeField] private Transform leftEnd;
    [SerializeField] private Transform astros;
    [SerializeField] private float speed;
    private Vector3 Scale;
    private bool leftMove;
    [SerializeField] private Animator anim;
    [SerializeField] private float Freeze;
    private float FreezeTime;

    private void Awake()
    {
        Scale = astros.localScale;
    }
    private void OnDisable()// Called when the object becomes disabled
    {
        anim.SetBool("moving", false);
    }
    private void Update()
    {
        if(leftMove)
        {
            if (astros.position.x >= leftEnd.position.x)//We know enemy hasn't reached the end of the left side
            {
                DirectMove(-1);
            }
            else
            {
                Change();
            }
        }
        else
        {
            if (astros.position.x <= rightEnd.position.x)//Keep going right til enemy reaches end
            {
                DirectMove(1);
            }
            else
            {
                Change();
            }
        }
        
    }

    private void Change()
    {//Turn around
        anim.SetBool("moving", false);
        FreezeTime += Time.deltaTime;
        if(FreezeTime > Freeze)
            leftMove = !leftMove;
    }
    private void DirectMove(int direct)
    {
        FreezeTime = 0;
        anim.SetBool("moving", true);
        //Face enemy in correct way
        astros.localScale = new Vector3(Mathf.Abs(Scale.x) * direct, Scale.y, Scale.z); // Mathf.abs to avoid initScale being positive or negative

        //Walk there
        astros.position = new Vector3(astros.position.x + Time.deltaTime * direct * speed, astros.position.y, astros.position.z);
    }
}
