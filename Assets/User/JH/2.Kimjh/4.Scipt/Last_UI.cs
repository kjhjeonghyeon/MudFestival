using System.Collections;
using UnityEngine;

public class Last_UI : MonoBehaviour
{
    public GameObject lastUI;


    // Start is called before the first frame update
    void Start()
    {
        lastUI.SetActive(false);
        StartCoroutine(ShowUI(20f));
    }

    IEnumerator ShowUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        lastUI.SetActive(true);
    }
}
    