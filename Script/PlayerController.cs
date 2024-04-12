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
    bool isDoubleClick = false; // ˫��״̬��־
    float lastClickTime = 0; // �ϴε��ʱ��

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

        // �������������Դ����ƶ�
        if (Input.GetMouseButtonDown(0))
        {
            // �����˫�������ε�����С��0.5�룩
            if (Time.time - lastClickTime < 0.5f)
            {
                jump = true; // ������Ծ
                isDoubleClick = true; // ����˫��״̬
            }
            else
            {
                // �������˫�������ƶ��������λ��
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // ��������λ���Ƿ��ڽ�ɫ�������Ҳ�
                if (mousePosition.x > transform.position.x)
                {
                    move = 1; // �����ƶ�

                   
                }
                else if (mousePosition.x < transform.position.x)
                {
                    move = -1; // �����ƶ�

                    
                }
                move *= speed; // Ӧ���ٶ�

                Debug.Log("���" + move);
                isDoubleClick = false; // ����˫����������˫��״̬
            }
            lastClickTime = Time.time; // �����ϴε��ʱ��
        }

        // �������Ʋ���
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
        // ֻ�е�����˫��ʱ��Ӧ���ƶ�
        if (!isDoubleClick)
        {
            cc.Move(move, jump); // Ӧ���ƶ�����Ծ
        }
        // �����������Ծ����������Ծ״̬��˫��״̬
        if (jump)
        {
            jump = false; // ������Ծ״̬
            isDoubleClick = false; // ����˫��״̬
        }
    }
}