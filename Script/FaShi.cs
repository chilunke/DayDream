using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaShi : MonoBehaviour
{
    public Transform pointA; // ��һ��λ��
    public Transform pointB; // �ڶ���λ��
    public float speed = 1.0f; // �ƶ��ٶ�

    private bool movingToB = true; // �ƶ������־

    void Update()
    {
        // �����ƶ������־����Ŀ��λ��
        Transform target = movingToB ? pointB : pointA;

        // �ƶ�����Ŀ��λ��
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // ����Ƿ񵽴�Ŀ��λ��
        if (transform.position == target.position)
        {
            // �л��ƶ�����
            movingToB = !movingToB;
        }
    }
}
