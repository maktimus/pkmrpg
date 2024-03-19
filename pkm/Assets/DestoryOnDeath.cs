using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnDeath : MonoBehaviour
{
    Stats stats;
    CheckSpawn spawnCount;
    GameObject pet;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        spawnCount = GetComponent<CheckSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.hp == 0)
        {
            spawnCount.ReduceSpawn();
            Stats player = pet.GetComponent<Stats>();
            player.GainExp(stats.level, 0);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pet")
        {
            pet = collision.gameObject;
        }
    }
}
