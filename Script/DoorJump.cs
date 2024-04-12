using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorJump : MonoBehaviour
{

    // 目标场景的名称
    public string targetScene = "JumpGame";

    // 当其他Collider2D进入这个触发器时调用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的对象是否有"Player"标签
        if (collision.CompareTag("Player"))
        {

            // 加载目标场景
            SceneManager.LoadScene(targetScene);

            // 找到摄像机跟随的脚本组件
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            // 如果找到了摄像机跟随的脚本组件
            if (cameraFollow != null)
            {
                // 销毁摄像机跟随的游戏对象
                Destroy(cameraFollow.gameObject);
            }

            // 销毁碰撞的对象（角色）
            Destroy(collision.gameObject);

        }
    }

}
