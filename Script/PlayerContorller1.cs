using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorller1 : MonoBehaviour
{
    CharacterController2D cc;

    public float speed = 5f;

    Animator anim;
    Rigidbody2D rigid;

    float move;
    bool jump;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController2D>();

        anim = GetComponent<Animator>();

        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        float temp = move;

        move *= speed;
        jump = Input.GetButton("Jump");

        //¶¯»­²¿·Ö
        if (cc.isGrounded)
        {
            anim.SetFloat("Speed", Mathf.Abs(temp));
            anim.SetBool("JumpUp", false);
            anim.SetBool("JumpDown", false);

        }
        else
        {
            Vector3 ve1 = rigid.velocity;
            if(ve1.y > 0)
            {
                anim.SetBool("JumpUp", true);
                anim.SetBool("JumpDown", false);
            }
            else
            {
                anim.SetBool("JumpUp", false);
                anim.SetBool("JumpDown", true);
            }
        }



    }
    private void FixedUpdate()
    {
        cc.Move(move, jump);
    }
}
