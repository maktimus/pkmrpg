using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject cam, mon;
    public Transform player, monTrans;
    public float x, y, z;
    Follow followScript;
    public bool followMon = false;

    // Start is called before the first frame update
    void Start()
    {
        followMon = false;
        followScript = mon.GetComponent<Follow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followMon == false)
        {
            transform.position = player.transform.position + new Vector3(x, y, z);
        }
        if (followScript.battle == true)
        {
            transform.position = monTrans.position + new Vector3(x, y, z);
            followMon = true;
        }
    }
}
