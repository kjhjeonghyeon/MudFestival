using UnityEngine;

public class DecalOpacity : MonoBehaviour
{
    public float duration = 10f;
    private float timer = 0f;
    private Material mat;

    void Start()
    {
        // material을 복사해서 개별 인스턴스로 사용
        mat = gameObject.GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        timer += Time.deltaTime;
     
        float fade = Mathf.Lerp(0f, -1f, (timer / duration));

        mat.SetFloat("_Opacity", fade);

        if (timer >= duration)
            Destroy(gameObject);
    }
}
