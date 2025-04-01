using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アクター操作・制御クラス
/// </summary>
public class ActorController : MonoBehaviour
{
    // オブジェクト・コンポーネント参照
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    public CameraController cameraController; // カメラ制御クラス

    public GameObject GameClearPanel;
    public GameObject GameOverPanel;

    // 移動関連変数
    [HideInInspector] public float xSpeed; // X方向移動速度
    [HideInInspector] public bool rightFacing; // 向いている方向(true.右向き false:左向き)

    // ジャンプ関連変数
    private int jumpCount = 0; // ジャンプ回数
    private const int maxJumpCount = 2; // 最大ジャンプ回数
    private bool isGrounded = false; // 接地状態

    // Start（オブジェクト有効化時に1度実行）
    void Start()
    {
        // コンポーネント参照取得
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // カメラ初期位置
        cameraController.SetPosition(transform.position);

        // 変数初期化
        rightFacing = true; // 最初は右向き
    }

    // Update（1フレームごとに1度ずつ実行）
    void Update()
    {
        // 左右移動処理
        MoveUpdate();
        // ジャンプ入力処理
        JumpUpdate();

        // 落下処理
        if (transform.position.y < -50)
        {
            GameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }

        // カメラに自身の座標を渡す
        cameraController.SetPosition(transform.position);
    }

    /// <summary>
    /// Updateから呼び出される左右移動入力処理
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
    /// Updateから呼び出されるジャンプ入力処理
    /// </summary>
    private void JumpUpdate()
    {
        if (jumpCount < maxJumpCount && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpPower = 10.0f; // ジャンプ力
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
            jumpCount++;

            // 接地状態を解除
            isGrounded = false;
        }
    }

    /// <summary>
    /// 接地判定処理
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 接地状態をリセット
            isGrounded = true;
            jumpCount = 0; // ジャンプ回数リセット
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

    // FixedUpdate（一定時間ごとに1度ずつ実行・物理演算用）
    private void FixedUpdate()
    {
        Vector2 velocity = rigidbody2D.velocity;
        velocity.x = xSpeed;
        rigidbody2D.velocity = velocity;
    }
}
