using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�N�^�[����E����N���X
/// </summary>
public class ActorController : MonoBehaviour
{
    // �I�u�W�F�N�g�E�R���|�[�l���g�Q��
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    public CameraController cameraController; // �J��������N���X

    public GameObject GameClearPanel;
    public GameObject GameOverPanel;

    // �ړ��֘A�ϐ�
    [HideInInspector] public float xSpeed; // X�����ړ����x
    [HideInInspector] public bool rightFacing; // �����Ă������(true.�E���� false:������)

    // �W�����v�֘A�ϐ�
    private int jumpCount = 0; // �W�����v��
    private const int maxJumpCount = 2; // �ő�W�����v��
    private bool isGrounded = false; // �ڒn���

    // Start�i�I�u�W�F�N�g�L��������1�x���s�j
    void Start()
    {
        // �R���|�[�l���g�Q�Ǝ擾
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �J���������ʒu
        cameraController.SetPosition(transform.position);

        // �ϐ�������
        rightFacing = true; // �ŏ��͉E����
    }

    // Update�i1�t���[�����Ƃ�1�x�����s�j
    void Update()
    {
        // ���E�ړ�����
        MoveUpdate();
        // �W�����v���͏���
        JumpUpdate();

        // ��������
        if (transform.position.y < -50)
        {
            GameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }

        // �J�����Ɏ��g�̍��W��n��
        cameraController.SetPosition(transform.position);
    }

    /// <summary>
    /// Update����Ăяo����鍶�E�ړ����͏���
    /// </summary>
    private void MoveUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            xSpeed = 6.0f;
            rightFacing = true;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            xSpeed = -6.0f;
            rightFacing = false;
            spriteRenderer.flipX = true;
        }
        else
        {
            xSpeed = 0.0f;
        }
    }

    /// <summary>
    /// Update����Ăяo�����W�����v���͏���
    /// </summary>
    private void JumpUpdate()
    {
        if (jumpCount < maxJumpCount && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpPower = 10.0f; // �W�����v��
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            jumpCount++;

            // �ڒn��Ԃ�����
            isGrounded = false;
        }
    }

    /// <summary>
    /// �ڒn���菈��
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �ڒn��Ԃ����Z�b�g
            isGrounded = true;
            jumpCount = 0; // �W�����v�񐔃��Z�b�g
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            GameClearPanel.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }
    }

    // FixedUpdate�i��莞�Ԃ��Ƃ�1�x�����s�E�������Z�p�j
    private void FixedUpdate()
    {
        Vector2 velocity = rigidbody2D.velocity;
        velocity.x = xSpeed;
        rigidbody2D.velocity = velocity;
    }
}
