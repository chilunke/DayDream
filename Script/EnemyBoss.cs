using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float topY; // 上方的y坐标
    public float bottomY; // 下方的y坐标
    public float speed = 3f; // 移动速度

    private bool movingUp = true; // 是否向上移动

    // 更新帧调用
    void Update()
    {
        // 根据移动方向，更新敌人的位置
        if (movingUp)
        {
            // 向上移动
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            // 如果到达或超过上方坐标，改变方向
            if (transform.position.y >= topY)
            {
                movingUp = false;
            }
        }
        else
        {
            // 向下移动
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            // 如果到达或超过下方坐标，改变方向
            if (transform.position.y <= bottomY)
            {
                movingUp = true;
            }
        }
    }
}

