using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBack : MonoBehaviour
{
    public string targetScene; // 目标场景的名称
    public Vector3 targetDoorPosition; // 目标场景中的门位置

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 确保碰撞的对象是玩家
        {
            DontDestroyOnLoad(other.gameObject); // 确保玩家对象在场景加载时不被销毁
            SceneManager.LoadScene(targetScene); // 加载目标场景
            SceneManager.sceneLoaded += OnSceneLoaded; // 注册场景加载完成后的回调
        }
    }

    // 场景加载完成后的回调方法
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == targetScene)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // 找到玩家对象
            player.transform.position = targetDoorPosition; // 设置玩家位置为目标门的位置
            SceneManager.sceneLoaded -= OnSceneLoaded; // 取消注册回调，避免重复调用
        }
    }


}
