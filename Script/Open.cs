using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject dialogueBox; // �Ի���UIԪ��

    void Start()
    {
        // ȷ���Ի����ڿ�ʼʱ���ɼ�
        dialogueBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �����ײ�Ķ����Ƿ�Ϊ���
        if (other.CompareTag("Player"))
        {
            // ��ʾ�Ի���
            dialogueBox.SetActive(true);

        }
    }
}
