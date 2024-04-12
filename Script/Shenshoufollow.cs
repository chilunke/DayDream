using UnityEngine;

public class Shenshoufollow : MonoBehaviour
{
    public float moveSpeed = 5f; // ���˵��ƶ��ٶ�
    public float detectionRadius = 5f; // ��ⷶΧ�뾶
  /*  public float attackRange = 1f; // ������Χ
    public int attackDamage = 10; // �����˺�
    public float attackCooldown = 1f; // ������ȴʱ��*/

    private Transform player; // ��ɫ��Transform���
    private bool isFollowing = false; // �Ƿ�����ɫ
    /*private bool canAttack = true; // �Ƿ���Թ���
    private Animator animator; // ����������*/

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // ���Ҵ��� "Player" ��ǩ�Ľ�ɫ����
        /*animator = GetComponent<Animator>(); // ��ȡ���˵Ķ���������*/
    }

    void Update()
    {
        if (player != null)
        {
            // ����ɫ�Ƿ�����ⷶΧ
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius)
            {
                isFollowing = true;
                /*animator.SetBool("isFollowing", true); // ���ö���״̬Ϊ����*/
            }
            else
            {
                isFollowing = false;
                /*animator.SetBool("isFollowing", false); // ���ö���״̬Ϊֹͣ����*/
            }

            // ������ڸ����ɫ
            if (isFollowing)
            {
                // ������˳����ɫ�ķ���
                Vector2 direction = player.position - transform.position;
                direction.Normalize();

                // ���ݽ�ɫ���ƶ�����������˵ĳ���
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1); // �����ƶ�ʱ����ת���˵ĳ���Ϊ��
                }
                else if (direction.x > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1); // �����ƶ�ʱ����ת���˵ĳ���Ϊ��
                }

                // �ƶ����˳����ɫ
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                /*// �����ɫ�ڹ�����Χ���ҿ��Թ���
                if (distanceToPlayer <= attackRange && canAttack)
                {
                    Attack(); // ������ɫ
                }*/
            }
        }
    }

    /*void Attack()
    {
        // ������������������еĻ���
        animator.SetTrigger("attack");

        // �Խ�ɫ����˺���������Ը���ʵ����Ϸ�߼�������չ��
        // ��������ɫ��һ����Ϊ "Health" ����������Գ����˺�
        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        // ���ù�����ȴʱ��
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }*/

   /* void ResetAttackCooldown()
    {
        canAttack = true;
    }
*/
    // ���Ƽ�ⷶΧ�Ŀ��ӻ�������
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
