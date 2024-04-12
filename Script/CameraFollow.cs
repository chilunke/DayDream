using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ��ɫ��Transform���
    public float smoothSpeed = 0.125f; // ����������ƽ���ٶ�
    // ��������ɫ�����ƫ����
    public Vector3 offset;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void LateUpdate()
    {
        if (player == null)
        {
            // �ڳ����в��ұ��Ϊ"Player"�Ķ���
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

           
        }
    }
}
