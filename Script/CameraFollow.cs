using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 角色的Transform组件
    public float smoothSpeed = 0.125f; // 摄像机跟随的平滑速度
    // 摄像机与角色的相对偏移量
    public Vector3 offset;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void LateUpdate()
    {
        if (player == null)
        {
            // 在场景中查找标记为"Player"的对象
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
