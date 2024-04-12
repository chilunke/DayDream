using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float topY; // �Ϸ���y����
    public float bottomY; // �·���y����
    public float speed = 3f; // �ƶ��ٶ�

    private bool movingUp = true; // �Ƿ������ƶ�

    // ����֡����
    void Update()
    {
        // �����ƶ����򣬸��µ��˵�λ��
        if (movingUp)
        {
            // �����ƶ�
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            // �������򳬹��Ϸ����꣬�ı䷽��
            if (transform.position.y >= topY)
            {
                movingUp = false;
            }
        }
        else
        {
            // �����ƶ�
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            // �������򳬹��·����꣬�ı䷽��
            if (transform.position.y <= bottomY)
            {
                movingUp = true;
            }
        }
    }
}

