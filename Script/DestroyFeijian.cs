using UnityEngine;
using UnityEngine.Tilemaps;

public class DestroyFeijian : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�����Ƿ���TilemapCollider2D���
        if (collision.collider is TilemapCollider2D)
        {
            // ���ٵ�ǰ��Ԥ����
            Destroy(gameObject);
        }
    }
}
