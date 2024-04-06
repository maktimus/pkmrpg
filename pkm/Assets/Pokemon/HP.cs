using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] GameObject health;

    public void SetHP(float hpNormalised)
    {
        health.transform.localScale = new Vector3(hpNormalised, 1f);
    }


}
