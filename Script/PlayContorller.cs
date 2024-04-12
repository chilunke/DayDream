using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PlayContorller : MonoBehaviour
{   // 目标位置变量
    public Vector3 targetPosition;

    // 移动速度变量，可以在Unity编辑器中调整
    public float moveSpeed = 5f;

    private Animator _animator;
    private Vector3 lastPosition;
    private Animator attack;
    private Animator Bigattack;

    // 用于确保角色唯一性的静态变量
    private static PlayContorller instance = null;

    public VisualEffect vfxRenderer;

    void Awake()
    {
        // 检查是否已存在角色实例
        if (instance != null)
        {
            // 如果存在，则销毁这个新的重复实例
            Destroy(gameObject);
        }
        else
        {
            // 如果不存在，设置这个实例为唯一实例，并确保不被销毁
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // 获取Animator组件和Rigidbody2D组件
        _animator = GetComponent<Animator>();
        
        // 初始化上一帧的位置
        lastPosition = transform.position;


        // 使角色在加载新场景时不被销毁
        DontDestroyOnLoad(gameObject);

        // 获取父对象的子对象
        Transform childTransform = transform.Find("Attack");

        if (childTransform != null)
        {
            // 获取子对象的组件
             attack = childTransform.GetComponent<Animator>();
            // 获取子对象的组件
            Bigattack = childTransform.GetComponent<Animator>();


        }
    }

    // 每帧调用一次
    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标位置并转换为世界坐标
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 设置目标位置的z坐标为当前角色的z坐标，以保持在同一平面
            targetPosition.z = transform.position.z;
            
        }

        // 移动角色到目标位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 检查角色是否移动
        if (transform.position != lastPosition)
        {
            // 角色正在移动
            _animator.SetBool("isMoving", true);

            attack.SetBool("isAttack", true);
        }
        else
        {
            // 角色静止
            _animator.SetBool("isMoving", false);
            attack.SetBool("isAttack", false);
        }

        // 更新上一帧的位置
        lastPosition = transform.position;

        vfxRenderer.SetVector3("ColliderPos",transform.position);

    }

}
