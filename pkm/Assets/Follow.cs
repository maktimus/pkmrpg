using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject wayPoint;

    private Vector3 wayPointPos, targetPos;
    public bool follow = true, battle = false, initiate = false;

    //speed should be set by stats
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        wayPoint = GameObject.Find("Player");
        Debug.Log(follow);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //follows player
        if(follow == true)
        {
            //sets player location
            wayPointPos = new Vector3(wayPoint.transform.position.x, wayPoint.transform.position.y, wayPoint.transform.position.z);
            //moves pet towards player location
            transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
        }
        //walks towards enemy
        if(initiate == true)
        {
            //moves towards enemy
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    //starts battle
    public void DoBattle(GameObject target)
    {
        //changes pet to move towards the target
        initiate = true;
        follow = false;
        //locates targets position
        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

    }

    //check if near player
    private void OnTriggerEnter(Collider other)
    {
        //if withn radius of player, stops following
        if (other.gameObject.tag == "FollowRad")
        {
            follow = false;
        }
        //same but with enemy
        if (other.gameObject.tag == "SearchRad" && initiate == true)
        {
            initiate = false;
            battle = true;
        }
    }

    //resumes following player when no longer within radius
    private void OnTriggerExit(Collider other)
    {
        if(battle == true)
        {
            return;
        }
        else
        follow = true;
    }
}
//FIX BATTLE 