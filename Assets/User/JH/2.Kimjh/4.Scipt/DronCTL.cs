using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
public class DronCTL : MonoBehaviour
{
    public UnityEvent<int> EndAction = null;
    public int Index;
   public void OnEndAnim()
    {
        EndAction?.Invoke(Index);
    }
}
