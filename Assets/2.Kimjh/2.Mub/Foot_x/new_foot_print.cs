using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_foot_print : MonoBehaviour
{

    public GameObject decalls;       // ���ڱ� �θ� ������Ʈ
    public GameObject decall;        // ���ڱ� ������

    public int maxFootprints = 200;  // �迭 ũ�� ����
    private GameObject[] footprints;
    private int currentIndex = 0;

    private Vector3 lastFootprintPosition;
    private bool isFirst = true;

    private void Start()
    {
        footprints = new GameObject[maxFootprints];
    }
    public void start()
    {
        Vector3 myPosition = transform.position;

        Vector3 instantiatePos = new Vector3(
            myPosition.x,
            myPosition.y + 0.5f,
            myPosition.z
        );

        if (isFirst)
        {
            lastFootprintPosition = myPosition;
            CreateFootprint(instantiatePos);
            isFirst = false;
            return;
        }

        float distance = Vector3.Distance(lastFootprintPosition, myPosition);

        if (distance > 0.5f)
        {
            Debug.Log(distance);
            CreateFootprint(instantiatePos);
            lastFootprintPosition = myPosition;
        }
    }


    void CreateFootprint(Vector3 pos)
    {
        GameObject fp = Instantiate(decall, pos, transform.rotation, decalls.transform);

        // �迭�� ����
        if (currentIndex < maxFootprints)
        {
            footprints[currentIndex] = fp;
            StartCoroutine(RemoveAfterTime(currentIndex, 30f));
            currentIndex++;
        }
    }

    IEnumerator RemoveAfterTime(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (footprints[index] != null)
        {
            Destroy(footprints[index]);
            footprints[index] = null;
        }
    }
}
