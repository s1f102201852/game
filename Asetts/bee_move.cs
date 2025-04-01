using System.Collections;
using UnityEngine;

public class BeeMove : MonoBehaviour
{
    private Vector3 pos; // �����ʒu

    public bool moveHorizontally = true; // �����ړ��������ړ���
    public float moveDistance = 2f;     // �ړ���
    public float waveHeight = 0.5f;     // �T�C���g�̍���
    public float moveSpeed = 1f;        // �ړ����x
    public float waveSpeed = 2f;        // �g�̑��x

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
