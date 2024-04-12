using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowJump : MonoBehaviour
{
    public Transform target; // �����Ҫ�����Ŀ��
    public float minX = -10f; // ��߽�
    public float maxX = 10f; // �ұ߽�
    public float minY = -10f; // �±߽�
    public float maxY = 10f; // �ϱ߽�

    void Update()
    {
        // ��ȡĿ���λ��
        Vector3 targetPos = target.position;

        // ʹ��Mathf.Clamp���������������x��y����
        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPos.y, minY, maxY);

        // �����������λ��
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
