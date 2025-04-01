using System.Collections;
using UnityEngine;

public class BeeMove : MonoBehaviour
{
    private Vector3 pos; // 初期位置

    public bool moveHorizontally = true; // 水平移動か垂直移動か
    public float moveDistance = 2f;     // 移動幅
    public float waveHeight = 0.5f;     // サイン波の高さ
    public float moveSpeed = 1f;        // 移動速度
    public float waveSpeed = 2f;        // 波の速度

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if (moveHorizontally)
        {
            float x = pos.x + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
            float y = pos.y + Mathf.Sin(Time.time * waveSpeed) * waveHeight;
            transform.position = new Vector3(x, y, pos.z);
        }
        else
        {
            float x = pos.x + Mathf.Sin(Time.time * waveSpeed) * waveHeight;
            float y = pos.y + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
            transform.position = new Vector3(x, y, pos.z);
        }
    }
}
