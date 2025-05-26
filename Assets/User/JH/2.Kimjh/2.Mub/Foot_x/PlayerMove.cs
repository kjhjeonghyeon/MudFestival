using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ����Ű �Է� �ޱ�
        float moveX = Input.GetAxis("Horizontal"); // A/D �Ǵ� ��/�� ����Ű
        float moveZ = Input.GetAxis("Vertical");   // W/S �Ǵ� ��/�Ʒ� ����Ű

        // �̵� ���� ���
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ);

        // �ӵ� ����
        rb.velocity = moveDirection * moveSpeed + new Vector3(0, rb.velocity.y, 0); // y�� �ӵ� ����
    }
}
