using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRange : MonoBehaviour
{
    public bool inRange = false;
    public GameObject indicator;
    
    public void InRange(bool check)
    {
        inRange = check;
        if(inRange == true)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
