using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorJump : MonoBehaviour
{

    // Ŀ�곡��������
    public string targetScene = "JumpGame";

    // ������Collider2D�������������ʱ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�Ķ����Ƿ���"Player"��ǩ
        if (collision.CompareTag("Player"))
        {

            // ����Ŀ�곡��
            SceneManager.LoadScene(targetScene);

            // �ҵ����������Ľű����
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            // ����ҵ������������Ľű����
            if (cameraFollow != null)
            {
                // ����������������Ϸ����
                Destroy(cameraFollow.gameObject);
            }

            // ������ײ�Ķ��󣨽�ɫ��
            Destroy(collision.gameObject);

        }
    }

}
