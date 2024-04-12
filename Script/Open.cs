using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject dialogueBox; // 对话框UI元素

    void Start()
    {
        // 确保对话框在开始时不可见
        dialogueBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查碰撞的对象是否为玩家
        if (other.CompareTag("Player"))
        {
            // 显示对话框
            dialogueBox.SetActive(true);

        }
    }
}
