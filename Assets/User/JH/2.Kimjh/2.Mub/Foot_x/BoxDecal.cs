using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDecal : MonoBehaviour
{
    public GameObject boundery;
    public GameObject player;
    public GameObject decalls;
    public GameObject decall;
   
    Vector3 playerPos;
    bool playerMove;
    bool decallReach;

    GameObject[] deallDestory;
      List<GameObject> footprints = new List<GameObject>();

    void Start()
    {
        playerPos = player.transform.position;

      
    }

    private void FixedUpdate()
    {
        if (playerPos != player.transform.position)
        {
            playerMove = true;
        }
        else
        {
            playerMove = false;
        }

        
        if (Vector3.Distance(playerPos, player.transform.position) <= 0.5f)
        {
            decallReach = false;

        }
        else
        {
            decallReach = true;
            playerPos = player.transform.position;
        }

     




    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerStay(Collider other)
    {

        if (playerMove && decallReach)
        {
            GameObject fp=Instantiate(decall, other.transform.position, other.transform.rotation, decalls.transform);
            footprints.Add(fp);
            StartCoroutine(RemoveAfterTime(fp,30f));
            //Instantiate(decall_L, other.transform.position, other.transform.rotation, decalls.transform);


        }
    }

    

    IEnumerator RemoveAfterTime(GameObject fp, float delay)
    {
        yield return new WaitForSeconds(delay);
        footprints.Remove(fp);
        Destroy(fp);
        
    }
   
}
