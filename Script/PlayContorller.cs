using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class PlayContorller : MonoBehaviour
{   // Ŀ��λ�ñ���
    public Vector3 targetPosition;

    // �ƶ��ٶȱ�����������Unity�༭���е���
    public float moveSpeed = 5f;

    private Animator _animator;
    private Vector3 lastPosition;
    private Animator attack;
    private Animator Bigattack;

    // ����ȷ����ɫΨһ�Եľ�̬����
    private static PlayContorller instance = null;

    public VisualEffect vfxRenderer;

    void Awake()
    {
        // ����Ƿ��Ѵ��ڽ�ɫʵ��
        if (instance != null)
        {
            // ������ڣ�����������µ��ظ�ʵ��
            Destroy(gameObject);
        }
        else
        {
            // ��������ڣ��������ʵ��ΪΨһʵ������ȷ����������
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // ��ȡAnimator�����Rigidbody2D���
        _animator = GetComponent<Animator>();
        
        // ��ʼ����һ֡��λ��
        lastPosition = transform.position;


        // ʹ��ɫ�ڼ����³���ʱ��������
        DontDestroyOnLoad(gameObject);

        // ��ȡ��������Ӷ���
        Transform childTransform = transform.Find("Attack");

        if (childTransform != null)
        {
            // ��ȡ�Ӷ�������
             attack = childTransform.GetComponent<Animator>();
            // ��ȡ�Ӷ�������
            Bigattack = childTransform.GetComponent<Animator>();


        }
    }

    // ÿ֡����һ��
    void Update()
    {
        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            // ��ȡ���λ�ò�ת��Ϊ��������
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ����Ŀ��λ�õ�z����Ϊ��ǰ��ɫ��z���꣬�Ա�����ͬһƽ��
            targetPosition.z = transform.position.z;
            
        }

        // �ƶ���ɫ��Ŀ��λ��
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // ����ɫ�Ƿ��ƶ�
        if (transform.position != lastPosition)
        {
            // ��ɫ�����ƶ�
            _animator.SetBool("isMoving", true);

            attack.SetBool("isAttack", true);
        }
        else
        {
            // ��ɫ��ֹ
            _animator.SetBool("isMoving", false);
            attack.SetBool("isAttack", false);
        }

        // ������һ֡��λ��
        lastPosition = transform.position;

        vfxRenderer.SetVector3("ColliderPos",transform.position);

    }

}
