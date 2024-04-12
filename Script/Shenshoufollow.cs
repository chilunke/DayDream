using UnityEngine;

public class Shenshoufollow : MonoBehaviour
{
    public float moveSpeed = 5f; // 敌人的移动速度
    public float detectionRadius = 5f; // 检测范围半径
  /*  public float attackRange = 1f; // 攻击范围
    public int attackDamage = 10; // 攻击伤害
    public float attackCooldown = 1f; // 攻击冷却时间*/

    private Transform player; // 角色的Transform组件
    private bool isFollowing = false; // 是否跟随角色
    /*private bool canAttack = true; // 是否可以攻击
    private Animator animator; // 动画控制器*/

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 查找带有 "Player" 标签的角色对象
        /*animator = GetComponent<Animator>(); // 获取敌人的动画控制器*/
    }

    void Update()
    {
        if (player != null)
        {
            // 检测角色是否进入检测范围
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRadius)
            {
                isFollowing = true;
                /*animator.SetBool("isFollowing", true); // 设置动画状态为跟随*/
            }
            else
            {
                isFollowing = false;
                /*animator.SetBool("isFollowing", false); // 设置动画状态为停止跟随*/
            }

            // 如果正在跟随角色
            if (isFollowing)
            {
                // 计算敌人朝向角色的方向
                Vector2 direction = player.position - transform.position;
                direction.Normalize();

                // 根据角色的移动方向调整敌人的朝向
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1); // 向左移动时，翻转敌人的朝向为左
                }
                else if (direction.x > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1); // 向右移动时，翻转敌人的朝向为右
                }

                // 移动敌人朝向角色
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                /*// 如果角色在攻击范围内且可以攻击
                if (distanceToPlayer <= attackRange && canAttack)
                {
                    Attack(); // 攻击角色
                }*/
            }
        }
    }

    /*void Attack()
    {
        // 触发攻击动画（如果有的话）
        animator.SetTrigger("attack");

        // 对角色造成伤害（这里可以根据实际游戏逻辑进行扩展）
        // 这里假设角色有一个名为 "Health" 的组件，可以承受伤害
        if (player != null)
        {
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        // 设置攻击冷却时间
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }*/

   /* void ResetAttackCooldown()
    {
        canAttack = true;
    }
*/
    // 绘制检测范围的可视化辅助线
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
