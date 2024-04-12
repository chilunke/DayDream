using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowJump : MonoBehaviour
{
    public Transform target; // 摄像机要跟随的目标
    public float minX = -10f; // 左边界
    public float maxX = 10f; // 右边界
    public float minY = -10f; // 下边界
    public float maxY = 10f; // 上边界

    void Update()
    {
        // 获取目标的位置
        Vector3 targetPos = target.position;

        // 使用Mathf.Clamp函数限制摄像机的x和y坐标
        float clampedX = Mathf.Clamp(targetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPos.y, minY, maxY);

        // 设置摄像机的位置
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
