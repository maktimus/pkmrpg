using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    public List<GameObject> wildMon;
    public int listNum = 0;
    private GameObject targetMon;
    public GameObject pet;
    Follow followScript;
   
    // Start is called before the first frame update
    void Start()
    {
        followScript = pet.GetComponent<Follow>();
    }

    private void Update()
    {
        //changes what mon to lock on
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //disables the indicator from previous mon
            targetMon.GetComponent<SearchRange>().InRange(false);
            listNum++;
            //goes back to the start of the list
            if (listNum >= wildMon.Count)
            {
                listNum = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && wildMon.Count >= 1)
        {
            followScript.DoBattle(targetMon);
            Debug.Log("Do Battle");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wildMon.Count >= 1)
        {

            targetMon = wildMon[listNum];
            IndexCheck(targetMon);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SearchRad")
        {
            wildMon.Add(other.gameObject);
            //want to call selected object in list and grab call function in their script
            //other.gameObject.GetComponent<SearchRange>().InRange(true);
        }
    }

    private void IndexCheck(GameObject mon)
    {
        targetMon.GetComponent<SearchRange>().InRange(true);
    }


    private void OnTriggerExit(Collider other)
    {
        wildMon.Remove(other.gameObject);
        other.gameObject.GetComponent<SearchRange>().InRange(false);
        listNum--;
        if(listNum <= 0)
        {
            listNum = 0;
        }
    }
}
