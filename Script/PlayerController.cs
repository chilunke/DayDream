using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController2D cc;

    public float speed = 5f;

    Animator anim;
    Rigidbody2D rigid;

    float move;
    bool jump;
    bool isDoubleClick = false; // 双击状态标志
    float lastClickTime = 0; // 上次点击时间

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
        move = Input.GetAxis("Horizontal") * speed;

        
        jump = Input.GetButton("Jump");

        // 检测鼠标左键点击以触发移动
        if (Input.GetMouseButtonDown(0))
        {
            // 如果是双击（两次点击间隔小于0.5秒）
            if (Time.time - lastClickTime < 0.5f)
            {
                jump = true; // 触发跳跃
                isDoubleClick = true; // 更新双击状态
            }
            else
            {
                // 如果不是双击，则移动到鼠标点击位置
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // 检查鼠标点击位置是否在角色的左侧或右侧
                if (mousePosition.x > transform.position.x)
                {
                    move = 1; // 向右移动

                   
                }
                else if (mousePosition.x < transform.position.x)
                {
                    move = -1; // 向左移动

                    
                }
                move *= speed; // 应用速度

                Debug.Log("鼠标" + move);
                isDoubleClick = false; // 不是双击，不更新双击状态
            }
            lastClickTime = Time.time; // 更新上次点击时间
        }

        // 动画控制部分
        if (cc.isGrounded)
        {
            anim.SetFloat("Speed", Mathf.Abs(move));
            anim.SetBool("JumpUp", false);
            anim.SetBool("JumpDown", false);
        }
        else
        {
            if (rigid.velocity.y > 0)
            {
                anim.SetBool("JumpUp", true);
                anim.SetBool("JumpDown", false);
            }
            else if (rigid.velocity.y < 0)
            {
                anim.SetBool("JumpUp", false);
                anim.SetBool("JumpDown", true);
            }
        }
    }

    private void FixedUpdate()
    {
        // 只有当不是双击时才应用移动
        if (!isDoubleClick)
        {
            cc.Move(move, jump); // 应用移动和跳跃
        }
        // 如果发生了跳跃，才重置跳跃状态和双击状态
        if (jump)
        {
            jump = false; // 重置跳跃状态
            isDoubleClick = false; // 重置双击状态
        }
    }
}