using UnityEngine;

public class muddd : MonoBehaviour
{
    [SerializeField] private float normalSpeed = 5.0f;
    [SerializeField] private float mudSpeed = 2.0f;
    private CharacterController controller;
    private bool isInMud = false;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        // 입력 처리
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // 이동 방향 계산
        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        
        // 현재 영역에 따른 속도 결정
        float currentSpeed = isInMud ? mudSpeed : normalSpeed;
        
        // 이동 실행
        controller.Move(move * currentSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mud"))
        {
            isInMud = true;
            Debug.Log("진흙에 들어왔습니다!");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mud"))
        {
            isInMud = false;
            Debug.Log("진흙에서 나왔습니다!");
        }
    }
}