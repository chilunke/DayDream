using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyFeijian : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞对象是否有TilemapCollider2D组件
        if (collision.collider is TilemapCollider2D)
        {
            // 销毁当前的预制体
            Destroy(gameObject);
        }
    }
}
