//using System.Collections;
//using UnityEngine;

//public class FootPrint : MonoBehaviour
//{
//    public GameObject decalls;       // ���ڱ� �θ� ������Ʈ
//    public GameObject decall;        // ���ڱ� ������

//    public int maxFootprints = 200;  // �迭 ũ�� ����
//    private GameObject[] footprints;
//    private int currentIndex = 0;

//    private Vector3 lastFootprintPosition;
//    private bool isFirst = true;

//    private void Start()
//    {
//        footprints = new GameObject[maxFootprints];
//    }

//    public void OnTriggerStay(Collider other)
//    {
//        Vector3 myPosition = other.transform.position;
//        Quaternion quaternion = other.transform.rotation;

//        Vector3 instantiatePos = new Vector3(
//            myPosition.x,
//            myPosition.y + 0.2f,
//            myPosition.z
//        );


//        if (isFirst)
//        {
//            lastFootprintPosition = myPosition;
//            CreateFootprint(instantiatePos, quaternion);
//            isFirst = false;
//            return;
//        }

//        float distance = Vector3.Distance(lastFootprintPosition, myPosition);

//        if (distance > 1f)
//        {
//            Debug.Log(distance);
//            CreateFootprint(instantiatePos, quaternion);
//            lastFootprintPosition = myPosition;

//        }
//    }

//    void CreateFootprint(Vector3 pos,Quaternion quaternion)
//    {
//        GameObject fp = Instantiate(decall, pos, quaternion, decalls.transform);

//        // �迭�� ����
//        if (currentIndex < maxFootprints)
//        {
//            footprints[currentIndex] = fp;
//            StartCoroutine(RemoveAfterTime(currentIndex, 30f));
//            currentIndex++;
//        }
//    }

//    IEnumerator RemoveAfterTime(int index, float delay)
//    {
//        yield return new WaitForSeconds(delay);

//        if (footprints[index] != null)
//        {
//            Destroy(footprints[index]);
//            footprints[index] = null;
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    public GameObject decalls;     // ���ڱ� �θ� ������Ʈ
    public GameObject decall;      // ���ڱ� ������

    public int maxFootprints = 200;
    private GameObject[] footprints;
    private int currentIndex = 0;

    private Dictionary<Transform, Vector3> lastPositions = new();
    private Dictionary<Transform, bool> isFirstFoot = new();

    private void Start()
    {
        footprints = new GameObject[maxFootprints];
    }

    public void OnTriggerStay(Collider other)
    {
        Transform foot = other.transform;

        // �ٴ� Raycast�� ��ġ ���
        if (!Physics.Raycast(foot.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, 1.5f))
            return;

        Vector3 groundPos = hit.point + Vector3.up * 0.2f;
        Quaternion rot = Quaternion.LookRotation(Vector3.ProjectOnPlane(foot.forward, hit.normal), hit.normal);

        // ó���̸� ��� ���
        if (!isFirstFoot.ContainsKey(foot) || isFirstFoot[foot])
        {
            CreateFootprint(groundPos, rot);
            lastPositions[foot] = foot.position;
            isFirstFoot[foot] = false;
            return;
        }

        float distance = Vector3.Distance(lastPositions[foot], foot.position);
        if (distance > 1f)
        {
            CreateFootprint(groundPos, rot);
            lastPositions[foot] = foot.position;
        }
    }

    void CreateFootprint(Vector3 pos, Quaternion rot)
    {
        GameObject fp = Instantiate(decall, pos, rot, decalls.transform);

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
