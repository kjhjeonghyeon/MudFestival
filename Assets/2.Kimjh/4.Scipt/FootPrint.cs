using System.Collections;
using UnityEngine;

public class FootPrint : MonoBehaviour
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

    public void OnTriggerStay(Collider other)
    {
        Vector3 myPosition = other.transform.position;
        Quaternion quaternion = other.transform.rotation;

        Vector3 instantiatePos = new Vector3(
            myPosition.x,
            myPosition.y + 0.2f,
            myPosition.z
        );
       

        if (isFirst)
        {
            lastFootprintPosition = myPosition;
            CreateFootprint(instantiatePos, quaternion);
            isFirst = false;
            return;
        }

        float distance = Vector3.Distance(lastFootprintPosition, myPosition);

        if (distance > 1f)
        {
            Debug.Log(distance);
            CreateFootprint(instantiatePos, quaternion);
            lastFootprintPosition = myPosition;

        }
    }

    void CreateFootprint(Vector3 pos,Quaternion quaternion)
    {
        GameObject fp = Instantiate(decall, pos, quaternion, decalls.transform);

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
