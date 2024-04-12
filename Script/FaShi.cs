using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaShi : MonoBehaviour
{
    public Transform pointA; // 第一个位置
    public Transform pointB; // 第二个位置
    public float speed = 1.0f; // 移动速度

    private bool movingToB = true; // 移动方向标志

    void Update()
    {
        // 根据移动方向标志决定目标位置
        Transform target = movingToB ? pointB : pointA;

        // 移动对象到目标位置
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // 检查是否到达目标位置
        if (transform.position == target.position)
        {
            // 切换移动方向
            movingToB = !movingToB;
        }
    }
}
