using UnityEngine;

public class Skybox: MonoBehaviour
{
    public float rotationSpeed = 1f; // 초당 회전 속도

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
