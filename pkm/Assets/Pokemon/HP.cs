using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    //Gets reference to HP bar
    [SerializeField] GameObject health;

    //Sets the HP value to the HP bar
    public void SetHP(float hpNormalised)
    {
        health.transform.localScale = new Vector3(hpNormalised, 1f);
    }


}
