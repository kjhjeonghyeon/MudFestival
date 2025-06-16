using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronAnim : MonoBehaviour
{
    public List<DronCTL> dron;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var d in dron)
        {
            d.EndAction.AddListener(DronPlay);
        }
    }



    public void DronPlay(int index)
    {
        Debug.Log("flag1");
     
        dron[index].gameObject.SetActive(false);


        if (dron.Count > index + 1)
        {
            Debug.Log("flag2");

            dron[index+1].gameObject.SetActive(true);
        }
        else
        {
            dron[0].gameObject.SetActive(true);
        }
    }
}
