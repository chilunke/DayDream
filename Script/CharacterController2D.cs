using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    public float jumpForce = 400f;                          // ������
    public bool canAirControl = false;                      // �ڿ���ʱ���Ƿ��ܿ���
    public LayerMask groundMask;                            // ������һ��Layer�ǵ���
    public Transform m_GroundCheck;                         // �����ж�����Ŀ�����

    const float k_GroundedRadius = .1f; // ���ڼ������СԲ�εİ뾶
    private bool m_Grounded;            // ��ǰ�Ƿ��ڵ�����
    private bool m_FacingRight = true;  // ����Ƿ��泯�ұ�
    private Vector3 m_Velocity = Vector3.zero;

    const float m_NextGroundCheckLag = 0.1f;    // �������һС��ʱ�䣬�����ٴ���������ֹ������һ�ֽ������
    float m_NextGroundCheckTime;            // �������ʱ��ſ�����ء������ٴ�����

    // �����ɫ������������������������
    private Rigidbody2D m_Rigidbody2D;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnAirEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public bool isGrounded { get; private set; }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (OnAirEvent == null)
            OnAirEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // �����������ײ
        if (Time.time > m_NextGroundCheckTime)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, groundMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }

        if (wasGrounded && !m_Grounded)
        {
            OnAirEvent.Invoke();
        }
    }


    public void Move(float move, bool jump)
    {
        // ����ڵ���ʱ�����߿��Կ��п���ʱ�������ƶ�
        if (m_Grounded || canAirControl)
        {
            // �������move���������ٶ�
            m_Rigidbody2D.velocity = new Vector2(move, m_Rigidbody2D.velocity.y);

            // �泯��ʱ����������泯��ʱ���Ҽ��������ý�ɫˮƽ��ת
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        // �ڵ���ʱ������Ծ�����ͻ���Ծ
        if (m_Grounded && jump)
        {
            OnAirEvent.Invoke();
            m_Grounded = false;
            // ʩ�ӵ�����
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            m_NextGroundCheckTime = Time.time + m_NextGroundCheckLag;
        }
    }


    private void Flip()
    {
        // true��false��false��true
        m_FacingRight = !m_FacingRight;

        // ���ŵ�x�����-1��ͼƬ��ˮƽ��ת��
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }

    private void Update()
    {
        isGrounded = m_Grounded;
    }
}