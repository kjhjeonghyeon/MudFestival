using UnityEngine;

public class Skybox: MonoBehaviour
{
    public float rotationSpeed = 1f; // �ʴ� ȸ�� �ӵ�

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
